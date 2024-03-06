using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static RotatorAuthoring;

/// <summary>
/// エンティティを回転させるシステム
/// RotatorDataコンポーネントを持つすべてのエンティティに対して、指定された速度で回転を適用
/// </summary>
public partial class RotatorSystem : SystemBase
{
    /// <summary>
    /// フレームごとにエンティティの回転を更新
    /// </summary>
    protected override void OnUpdate()
    {
        // デルタタイムを取得
        float deltaTime = SystemAPI.Time.DeltaTime;

        // Entities.ForEachを使って、すべてのエンティティをループします。
        Entities
            .ForEach((ref LocalTransform rotation, in RotatorData rotator) =>
            {
                // エンティティの回転を更新 , Y軸にrotator.speed * deltaTimeの速度で回転
                rotation.Rotation = math.mul(
                    math.normalize(rotation.Rotation), 
                    quaternion.Euler(0f, math.radians(rotator.speed * deltaTime), 0f)); 
            })
           // 並列にスケジュール
           .ScheduleParallel(); 
    }
}
using Unity.Entities;
using Unity.Burst;
using Unity.Mathematics;

/// <summary>
/// 敵の状態を更新するシステム
/// 敵がダメージを受けた際のヒット有効時間を減少させ、0になったらヒット状態を解除
/// </summary>
[BurstCompile]
public partial struct EnemyUpdateSystem : ISystem
{
    /// <summary>
    /// システムを初期化する。EnemyDataコンポーネントが存在するエンティティがアクティブな時のみ更新するように設定
    /// </summary>
    [BurstCompile]
    void ISystem.OnCreate(ref SystemState state)
    {
        // システムの更新要件を設定
        state.RequireForUpdate<EnemyData>();
    }

    /// <summary>
    /// 敵エンティティの状態更新を行う
    /// 各フレームごとに敵のヒット有効時間を減少させ、0以下になったらヒット状態をリセット
    /// </summary>
    [BurstCompile]
    void ISystem.OnUpdate(ref SystemState state)
    {
        // デルタタイムを取得
        float deltaTime = SystemAPI.Time.DeltaTime;
        
        // 全ての敵エンティティに対して処理を実行
        foreach (var query in SystemAPI.Query<RefRW<EnemyData>>())
        {
            // 現在の敵データを取得
            EnemyData enemyData = query.ValueRO;
            
            // ヒット有効時間を減少させる
            enemyData.NoDamageTimeLeft = math.max(enemyData.NoDamageTimeLeft - deltaTime, 0f);

            // 更新された敵データを設定
            query.ValueRW = enemyData;
        }
    }
}
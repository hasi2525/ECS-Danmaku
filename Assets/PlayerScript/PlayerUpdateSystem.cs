using Unity.Mathematics;
using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;
using static PlayerAuthoring;

/// <summary>
/// PlayerUpdateSystemは、プレイヤーの位置を入力と移動速度に基づいて更新
/// Burstコンパイルを利用して高速化され、SimulationSystemGroup で更新
/// </summary>
[BurstCompile]
[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial struct PlayerUpdateSystem : ISystem
{
    /// <summary>
    /// システムが動作するために必要なコンポーネントデータの要件を設定
    /// </summary>
    [BurstCompile]
    void ISystem.OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerMoveData>();
        state.RequireForUpdate<PlayerStatusData>();
        state.RequireForUpdate<LocalTransform>();
    }

    /// <summary>
    /// プレイヤーの入力と移動速度を基にエンティティの位置を更新
    /// SystemAPI.Queryを使用して関連するコンポーネントデータをクエリし、プレイヤーの新しい位置を計算
    /// </summary>
    [BurstCompile]
    void ISystem.OnUpdate(ref SystemState state)
    {
        // 現在のフレームと前のフレームとの時間差を取得
        float deltaTime = SystemAPI.Time.DeltaTime;

        // シングルトンとして保存されているプレイヤーの移動入力データを取得
        PlayerMoveData playerInputData = SystemAPI.GetSingleton<PlayerMoveData>();

        // プレイヤーの入力に基づいて計算された移動量
        float3 movement = new float3(playerInputData.Move.x, 0, playerInputData.Move.y) * deltaTime;

        // プレイヤーの移動速度データ (PlayerStatusData) と位置情報 (LocalTransform) を持つすべてのエンティティに対してループ
        foreach ((RefRO<PlayerStatusData> playerParam, RefRW<LocalTransform> localTransform) in SystemAPI.Query<RefRO<PlayerStatusData>, RefRW<LocalTransform>>())
        {
            // プレイヤーの移動速度を取得
            float moveSpeed = playerParam.ValueRO.MoveSpeed;
            // 新しい位置を計算し、LocalTransform コンポーネントに設定
            localTransform.ValueRW.Position += movement * moveSpeed;
        }
    }
}
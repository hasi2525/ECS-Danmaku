using Unity.Entities;
using Unity.Burst;
using Unity.Physics.Systems;
using Unity.Transforms;
using Unity.Physics;
using Unity.Collections;
using static BulletAuthoring;

/// <summary>
/// 弾丸が他のエンティティと衝突したイベントを処理するシステム
/// 衝突した弾丸を破棄し、衝突対象に応じた処理を実行
/// </summary>
[BurstCompile]
[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
// 物理挙動のシステムの後に実行する
[UpdateAfter(typeof(PhysicsSystemGroup))]
public partial struct BulletTriggerEventSystem : ISystem
{
    // 各コンポーネントのデータにアクセスするためのLookup
    private ComponentLookup<LocalTransform> _localTransformLookup;
    private ComponentLookup<BulletData> _bulletLookup;
    private ComponentLookup<EnemyData> _enemyLookup;

    [BurstCompile]
    void ISystem.OnCreate(ref SystemState state)
    {
        //システムが更新されるために必要なコンポーネントタイプを指定
        state.RequireForUpdate<SimulationSingleton>();
        state.RequireForUpdate<LocalTransform>();
        state.RequireForUpdate<BulletData>();

        // 各コンポーネントデータへの参照を取得
        _localTransformLookup = state.GetComponentLookup<LocalTransform>(true);
        _bulletLookup = state.GetComponentLookup<BulletData>(true);
        _enemyLookup = state.GetComponentLookup<EnemyData>(true);
    }

    [BurstCompile]
    void ISystem.OnUpdate(ref SystemState state)
    {
        // Lookupの更新
        _localTransformLookup.Update(ref state);
        _bulletLookup.Update(ref state);
        _enemyLookup.Update(ref state);

        // エンティティマネージャとシミュレーションシングルトンの取得
        EntityManager entityManager = state.EntityManager;
        SimulationSingleton simulationSingleton = SystemAPI.GetSingleton<SimulationSingleton>();

        //EntityCommandBufferを取得
        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer
        (Allocator.TempJob, PlaybackPolicy.MultiPlayback);

        // 衝突判定処理
        new BulletTriggerEventJob
        {
            LocalTransformLookup = _localTransformLookup,
            BulletLookup = _bulletLookup,
            EnemyLookup = _enemyLookup,
            EntityManager = entityManager,
            EntityCommandBuffer = entityCommandBuffer
        }
        .Schedule(simulationSingleton, state.Dependency)
        .Complete();

        // コマンドバッファの実行
        entityCommandBuffer.Playback(entityManager);
        entityCommandBuffer.Dispose();
    }
}
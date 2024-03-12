using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;
using static BulletAuthoring;

/// <summary>
/// 弾の移動と自動破壊を処理するシステム
/// BulletDataコンポーネントを持つすべてのエンティティに対して、
/// 指定された速度で移動させ、設定した距離を超えると自動的に破壊
/// </summary>
[UpdateInGroup(typeof(SimulationSystemGroup))]
[BurstCompile]
public partial struct BulletUpdateSystem : ISystem
{
    private EntityQuery _entityQuery;

    /// <summary>
    /// システムの初期化時に呼び出されるメソッド。必要なコンポーネントでエンティティクエリを作成
    /// </summary>
    [BurstCompile]
    void ISystem.OnCreate(ref SystemState state)
    {
        // コンポーネントタイプの配列を作成し、クエリ対象のコンポーネントを指定
        NativeArray<ComponentType> components = new NativeArray<ComponentType>(2, Allocator.Temp);
        components[0] = ComponentType.ReadWrite<BulletData>();
        components[1] = ComponentType.ReadWrite<LocalTransform>();

        // エンティティクエリを作成
        _entityQuery = state.GetEntityQuery(components);

        // 一時的に作成した配列を解放
        components.Dispose();
    }
    /// <summary>
    /// フレームごとに呼び出され、弾丸の移動と自動破壊を処理
    /// </summary>
    /// 
    [BurstCompile]
    void ISystem.OnUpdate(ref SystemState state)
    {
        // EntityManagerとEntityCommandBufferを取得
        EntityManager entityManager = state.EntityManager;
        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp, PlaybackPolicy.MultiPlayback);

        // Time.DeltaTimeを取得
        float deltaTime = SystemAPI.Time.DeltaTime;

        // クエリにマッチするエンティティの配列を取得
        NativeArray<Entity> entities = _entityQuery.ToEntityArray(Allocator.Temp);
        foreach (Entity entity in entities)
        {
            // エンティティからBulletDataとLocalTransformコンポーネントを取得
            BulletData bulletData = entityManager.GetComponentData<BulletData>(entity);
            LocalTransform localTransform = entityManager.GetComponentData<LocalTransform>(entity);

            // 弾の移動距離を計算し、弾の自動破壊距離を更新
            float movement = bulletData.Speed * deltaTime;
            bulletData.AutoDestroyDistance -= movement;

            // 弾の新しい位置を計算して更新
            float3 deltaPosition = localTransform.Forward() * movement;
            localTransform.Position += deltaPosition;

            // 弾の移動情報と位置情報を更新
            entityManager.SetComponentData(entity, bulletData);
            entityManager.SetComponentData(entity, localTransform);

            // 自動破壊距離が0以下になったら、エンティティを破壊
            if (bulletData.AutoDestroyDistance <= 0f)
            {
                entityCommandBuffer.DestroyEntity(entity);
            }
        }

        // コマンドバッファを実行し、エンティティの変更を適用
        entityCommandBuffer.Playback(entityManager);

        // 使用したリソースを解放
        entities.Dispose();
        entityCommandBuffer.Dispose();
    }
}
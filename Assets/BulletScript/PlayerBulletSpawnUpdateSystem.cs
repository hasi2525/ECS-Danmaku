using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Transforms;
using UnityEngine;

// Burstコンパイラを使用して、このシステムの実行を最適化
[BurstCompile]
//[UpdateInGroup(typeof(SimulationCharaSystemGroup))]
public partial struct PlayerBulletSpawnUpdateSystem : ISystem
{
    // エンティティクエリを格納するためのプライベートフィールド
    private EntityQuery _entityQuery; 

    // システムの作成時に一度だけ呼び出されます。このシステムで必要なコンポーネントタイプを指定してクエリを構築
    [BurstCompile]
    void ISystem.OnCreate(ref SystemState state)
    {
        // 必要なコンポーネントタイプの配列を作成
        var components = new NativeArray<ComponentType>(2, Allocator.Temp);
        // 弾丸発射データの読み書きが可能なコンポーネント
        components[0] = ComponentType.ReadWrite<PlayerBulletSpawnData>();
        // 読み取り専用のローカルからワールドへの変換コンポーネント
        components[1] = ComponentType.ReadOnly<LocalToWorld>();

        // 指定されたコンポーネントを持つエンティティに対するクエリを作成
        _entityQuery = state.GetEntityQuery(components);
        components.Dispose(); // 一時配列を破棄して、メモリを解放
    }

    [BurstCompile]
    void ISystem.OnUpdate(ref SystemState state)
    {
        // EntityManagerを取得して、エンティティのコンポーネントを管理
        EntityManager entityManager = state.EntityManager;
        // EntityCommandBufferを使用して、エンティティの作成やコンポーネントの変更をキューに入れ、一括で処理
        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp, PlaybackPolicy.MultiPlayback);

        // 現在のフレームのデルタタイムを取得
        float deltaTime = SystemAPI.Time.DeltaTime;
        // クエリに一致するすべてのエンティティを配列として取得
        var entities = _entityQuery.ToEntityArray(Allocator.Temp);
        foreach (Entity entity in entities)
        {
            // 弾の発射データを持つエンティティから、そのデータを取得
            var spawner = entityManager.GetComponentData<PlayerBulletSpawnData>(entity);

            //左クリックを押している間弾を発射
            if (Input.GetMouseButton(0))
            {
                // 弾の発射間隔をカウントダウンします。
                if (spawner.BulletSpawnCurrentTime > 0f)
                {
                    spawner.BulletSpawnCurrentTime -= deltaTime;
                    entityManager.SetComponentData(entity, spawner);
                    // まだ発射時間に達していない場合は、次のエンティティに移る
                    continue;
                }

                // 発射時間に達したら、次の発射までの時間をリセット
                spawner.BulletSpawnCurrentTime = spawner.BulletSpawnInterval;
                entityManager.SetComponentData(entity, spawner);

                // 弾のエンティティを生成します。弾の原型（プロトタイプ）から新しいエンティティをインスタンス化
                LocalToWorld spawnerLtw = entityManager.GetComponentData<LocalToWorld>(entity);
                Entity bulletEntity = entityCommandBuffer.Instantiate(spawner.BulletPrototype);

                // 弾の位置と回転を、発射元のエンティティに基づいて設定
                var bulletTransform = LocalTransform.FromPositionRotation(spawnerLtw.Position, spawnerLtw.Rotation);
                entityCommandBuffer.AddComponent(bulletEntity, bulletTransform);
            }

        }

        // EntityCommandBufferに溜まったコマンド（エンティティの生成やコンポーネントの追加など）を実行
        entityCommandBuffer.Playback(entityManager);
        // 使用したリソースを解放
        entities.Dispose();
        entityCommandBuffer.Dispose();
    }
}
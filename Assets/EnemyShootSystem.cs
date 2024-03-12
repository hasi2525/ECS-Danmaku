//using Unity.Entities;
//using Unity.Burst;
//using Unity.Transforms;
//using Unity.Mathematics;
//using Unity.Collections;

//[BurstCompile]
//public partial struct EnemyShootSystem : ISystem
//{
//    public void OnCreate(ref SystemState state)
//    {
//        // システムの初期化処理
//    }

//    public void OnDestroy(ref SystemState state)
//    {
//        // システムの破棄処理
//    }

//    [BurstCompile]
//    public void OnUpdate(ref SystemState state)
//    {
//        // EntityManagerの取得
//        var entityManager = state.EntityManager;

//        // EntityCommandBufferの取得
//        var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);

//        // プレイヤーのEntityを見つける（仮のコード、プレイヤーが特定のTagコンポーネントを持っていると仮定）
//        EntityQuery playerQuery = entityManager.CreateEntityQuery(typeof(PlayerTag));
//        Entity playerEntity = playerQuery.GetSingletonEntity();
//        LocalToWorld playerLtw = entityManager.GetComponentData<LocalToWorld>(playerEntity);

//        // 敵が弾を発射する処理
//        Entities
//            .WithName("EnemyShoot")
//            .WithAll<EnemyTag>()
//            .ForEach((Entity enemyEntity, in LocalToWorld enemyLtw) =>
//            {
//                // プレイヤーの方向を計算
//                float3 direction = math.normalize(playerLtw.Position - enemyLtw.Position);

//                // 弾のプレハブのEntityを生成（仮のEntity、実際にはプレハブから変換したEntityを指定）
//                Entity bulletPrefabEntity = Entity.Null; // 仮の値

//                // 弾Entityの生成
//                Entity bulletEntity = entityCommandBuffer.Instantiate(bulletPrefabEntity);

//                // 弾の位置と向きを設定
//                LocalToWorld bulletLtw = new LocalToWorld
//                {
//                    Value = float4x4.TRS(
//                        enemyLtw.Position, // 弾の初期位置は敵の位置
//                        quaternion.LookRotationSafe(direction, math.up()), // 弾の向きはプレイヤーの方向
//                        new float3(1.0f, 1.0f, 1.0f)) // スケールは1
//                };

//                // 弾EntityにLocalToWorldコンポーネントを設定
//                entityCommandBuffer.SetComponent(bulletEntity, bulletLtw);

//            }).Schedule();

//        // EntityCommandBufferの実行
//        entityCommandBuffer.Playback(entityManager);
//        entityCommandBuffer.Dispose();
//    }
//}

////public struct EnemyTag : IComponentData { }
////public struct PlayerTag : IComponentData { }
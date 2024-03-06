using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using static BulletAuthoring;

/// <summary>
/// 弾と他のエンティティが衝突した際のイベントを処理するジョブ
/// 弾と衝突したエンティティ（敵など）の処理を行う
/// </summary>
[BurstCompile]
public struct BulletTriggerEventJob : ITriggerEventsJob
{
    // 敵エンティティのデータにアクセスするためのコンポーネントルックアップ
    [ReadOnly]
    public ComponentLookup<EnemyData> EnemyLookup;

    // 位置情報にアクセスするためのコンポーネントルックアップ
    [ReadOnly]
    public ComponentLookup<LocalTransform> LocalTransformLookup;

    // 弾のデータにアクセスするためのコンポーネントルックアップ
    [ReadOnly]
    public ComponentLookup<BulletData> BulletLookup;

    // エンティティを操作するためのエンティティマネージャ
    public EntityManager EntityManager;

    // エンティティの変更を一括で行うためのコマンドバッファ
    public EntityCommandBuffer EntityCommandBuffer;

    /// <summary>
    /// トリガーイベント（衝突イベント）が発生したときに実行
    /// 弾丸と他のエンティティの衝突処理を行う
    /// </summary>
    /// <param name="triggerEvent">衝突イベントのデータ</param>
    [BurstCompile]
    public void Execute(TriggerEvent triggerEvent)
    {
        // 衝突した2つのEntityを取り出す
        Entity entityA = triggerEvent.EntityA;
        Entity entityB = triggerEvent.EntityB;

        // どちらが弾丸かを調べる
        bool isBulletA = BulletLookup.HasComponent(entityA);
        bool isBulletB = BulletLookup.HasComponent(entityB);

        // 両方またはどちらも弾でない場合は処理を終了
        if (isBulletA == isBulletB) return;

        // 弾のエンティティを特定
        Entity bulletEntity = isBulletA ? entityA : entityB;

        // 弾を破棄します
        EntityCommandBuffer.DestroyEntity(bulletEntity);

        // 衝突したのが敵だった場合、敵のデータを更新
        if (EnemyLookup.TryGetComponent(entityA, out EnemyData enemyData))
        {
            // 弾のデータを取得し、敵のHPを減らす
            BulletData bulletData = BulletLookup[bulletEntity];
            enemyData.CurrentHp -= bulletData.Damage;

            // 被弾状態を短時間維持
            enemyData.NoDamageTimeLeft = 0.1f;
            EntityCommandBuffer.SetComponent(entityA, enemyData);

            // HPが0以下になった敵は破棄
            if (enemyData.CurrentHp <= 0)
            {
                EntityCommandBuffer.DestroyEntity(entityA);
            }
        }
    }
}
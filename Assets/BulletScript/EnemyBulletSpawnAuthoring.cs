using Unity.Entities;
using UnityEngine;

/// <summary>
/// プレイヤーの弾発射設定を行うためのオーサリングコンポーネント
/// このコンポーネントをGameObjectにアタッチしてUnityエディターから弾発射のプロパティを定義し、
/// ECSの世界で利用するためにエンティティのデータとして変換
/// </summary>
public class EnemyBulletSpawnAuthoring : MonoBehaviour
{
    // 発射する弾のプレハブ
    [SerializeField]
    private GameObject bulletPrefab;
    // 弾を発射する間隔
    [SerializeField]
    private float spawnInterval;

    /// <summary>
    /// オーサリングデータをECSのコンポーネントデータに変換するためのBakerクラス
    /// </summary>
    private class Baker : Baker<EnemyBulletSpawnAuthoring>
    {
        /// <summary>
        /// PlayerBulletSpawnAuthoringコンポーネントのデータをECSのコンポーネントデータに変換
        /// </summary>
        /// <param name="authoring">変換元のオーサリングコンポーネント</param>
        public override void Bake(EnemyBulletSpawnAuthoring authoring)
        {
            // 弾のプレファブをECSのエンティティに変換
            Entity bulletPrototype = GetEntity(authoring.bulletPrefab, TransformUsageFlags.Dynamic);

            //// 発射するエンティティ用のフラグ。動的な動きを示す
            //TransformUsageFlags flags = TransformUsageFlags.Dynamic;
            // オーサリングコンポーネントを持つGameObjectからエンティティを取得
            Entity entity = GetEntity(authoring, TransformUsageFlags.Dynamic);

            // 弾発射設定を持つコンポーネントデータを作成し、エンティティに追加
            EnemyBulletSpawnData playerBulletSpawnData = new EnemyBulletSpawnData
            {
                BulletPrototype = bulletPrototype,
                // 弾を発射する間隔
                BulletSpawnInterval = authoring.spawnInterval,
                // 発射までの現在のタイマーをリセット
                BulletSpawnCurrentTime = 0f,
            };
            // エンティティにPlayerBulletSpawnDataコンポーネントを追加
            AddComponent(entity, playerBulletSpawnData);
        }
    }
}
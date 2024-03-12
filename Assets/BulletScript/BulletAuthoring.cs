using UnityEngine;
using Unity.Entities;

/// <summary>
/// 弾の設定をUnityのInspector上で設定
/// ECSのエンティティに必要なコンポーネントデータを追加
/// </summary>
public class BulletAuthoring : MonoBehaviour
{
    // 弾の速度
    [SerializeField]
    private float speed;
    // 弾が与えるダメージ
    [SerializeField]
    private int damage;
    // 弾が自動的に破壊される距離
    [SerializeField]
    private float autoDestroyDistance; 

    /// <summary>
    /// BulletAuthoring のデータをもとに、エンティティにBulletDataコンポーネントを追加するBakerクラス
    /// </summary>
    private class Baker : Baker<BulletAuthoring>
    {
        public override void Bake(BulletAuthoring authoring)
        {
            // 弾の GameObject を動的なエンティティに変換
            Entity entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
            // Entity.Nullチェック
            if (entity == Entity.Null) return;

            // 弾のエンティティに BulletData コンポーネントを追加
            AddComponent(entity, new BulletData
            {
                Speed = authoring.speed,
                Damage = authoring.damage,
                AutoDestroyDistance = authoring.autoDestroyDistance
            });
        }
    }

    /// <summary>
    /// 弾に関連するデータを保持するECSコンポーネント
    /// </summary>
    public struct BulletData : IComponentData
    {
        // 弾の速度
        public float Speed;
        // 弾によって与えられるダメージ
        public int Damage;
        // 自動破壊までの距離
        public float AutoDestroyDistance;
    }
}
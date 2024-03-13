using Unity.Entities;
using UnityEngine;

public class PlayerTagAuthoring : MonoBehaviour
{
    private class Baker : Baker<PlayerTagAuthoring>
    {
        public override void Bake(PlayerTagAuthoring authoring)
        {
            // 弾の GameObject を動的なエンティティに変換
            Entity entity = GetEntity(authoring, TransformUsageFlags.None);

            // 弾のエンティティに BulletData コンポーネントを追加
            AddComponent(entity, new PlayerTag
            {
            });
        }
    }
}

public struct PlayerTag : IComponentData
{
}
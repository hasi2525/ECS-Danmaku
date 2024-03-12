using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerTag : MonoBehaviour
{
    private class Baker : Baker<PlayerTag>
    {
        public override void Bake(PlayerTag authoring)
        {
            // 弾の GameObject を動的なエンティティに変換
            Entity entity = GetEntity(authoring, TransformUsageFlags.None);

            // 弾のエンティティに BulletData コンポーネントを追加
            AddComponent(entity, new PlayerTag2
            {
            });
        }
    }
}

public struct PlayerTag2 : IComponentData
{
}
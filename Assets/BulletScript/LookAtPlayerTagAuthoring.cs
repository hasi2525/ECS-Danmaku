using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class LookAtPlayerTagAuthoring : MonoBehaviour
{
    private class Baker : Baker<LookAtPlayerTagAuthoring>
    {
        public override void Bake(LookAtPlayerTagAuthoring authoring)
        {
            // 弾の GameObject を動的なエンティティに変換
            Entity entity = GetEntity(authoring, TransformUsageFlags.None);

            // 弾のエンティティに BulletData コンポーネントを追加
            AddComponent(entity, new LookAtPlayerTag
            {
            });
        }
    }
}

public struct LookAtPlayerTag : IComponentData
{
}
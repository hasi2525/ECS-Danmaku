using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class EnemyTag : MonoBehaviour                    
{
    private class Baker : Baker<EnemyTag>
    {
        public override void Bake(EnemyTag authoring)
        {
            // 弾の GameObject を動的なエンティティに変換
            Entity entity = GetEntity(authoring, TransformUsageFlags.None);

            // 弾のエンティティに BulletData コンポーネントを追加
            AddComponent(entity, new EnemyTag2
            {
            });
        }
    }
}

public struct EnemyTag2 : IComponentData
{
}
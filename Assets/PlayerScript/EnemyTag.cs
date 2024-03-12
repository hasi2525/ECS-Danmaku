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
            // �e�� GameObject �𓮓I�ȃG���e�B�e�B�ɕϊ�
            Entity entity = GetEntity(authoring, TransformUsageFlags.None);

            // �e�̃G���e�B�e�B�� BulletData �R���|�[�l���g��ǉ�
            AddComponent(entity, new EnemyTag2
            {
            });
        }
    }
}

public struct EnemyTag2 : IComponentData
{
}
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
            // �e�� GameObject �𓮓I�ȃG���e�B�e�B�ɕϊ�
            Entity entity = GetEntity(authoring, TransformUsageFlags.None);

            // �e�̃G���e�B�e�B�� BulletData �R���|�[�l���g��ǉ�
            AddComponent(entity, new LookAtPlayerTag
            {
            });
        }
    }
}

public struct LookAtPlayerTag : IComponentData
{
}
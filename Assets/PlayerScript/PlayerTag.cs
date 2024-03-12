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
            // �e�� GameObject �𓮓I�ȃG���e�B�e�B�ɕϊ�
            Entity entity = GetEntity(authoring, TransformUsageFlags.None);

            // �e�̃G���e�B�e�B�� BulletData �R���|�[�l���g��ǉ�
            AddComponent(entity, new PlayerTag2
            {
            });
        }
    }
}

public struct PlayerTag2 : IComponentData
{
}
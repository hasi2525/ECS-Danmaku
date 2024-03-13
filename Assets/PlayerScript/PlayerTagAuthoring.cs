using Unity.Entities;
using UnityEngine;

public class PlayerTagAuthoring : MonoBehaviour
{
    private class Baker : Baker<PlayerTagAuthoring>
    {
        public override void Bake(PlayerTagAuthoring authoring)
        {
            // �e�� GameObject �𓮓I�ȃG���e�B�e�B�ɕϊ�
            Entity entity = GetEntity(authoring, TransformUsageFlags.None);

            // �e�̃G���e�B�e�B�� BulletData �R���|�[�l���g��ǉ�
            AddComponent(entity, new PlayerTag
            {
            });
        }
    }
}

public struct PlayerTag : IComponentData
{
}
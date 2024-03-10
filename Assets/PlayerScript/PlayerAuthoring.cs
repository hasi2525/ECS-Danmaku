using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// PlayerAuthoring �R���|�[�l���g�́AGameObject��ECS�̃G���e�B�e�B�ɕϊ����A
/// �v���C���[�Ɋ֘A����f�[�^�R���|�[�l���g��ǉ����邽�߂Ɏg�p���܂��B
/// </summary>
public class PlayerAuthoring : MonoBehaviour
{
    // �v���C���[�̈ړ����x
    [SerializeField]
    private float moveSpeed = 5.0f;

    private class Baker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            TransformUsageFlags flags = TransformUsageFlags.Dynamic;
            Entity entity = GetEntity(authoring, flags);

            // �v���C���[�G���e�B�e�B��PlayerStatusData�R���|�[�l���g��ǉ�
            AddComponent(entity, new PlayerStatusData
            {
                MoveSpeed = authoring.moveSpeed
            });

            // �v���C���[�G���e�B�e�B��PlayerMoveData�R���|�[�l���g��ǉ����A�����l��ݒ�
            AddComponent(entity, new PlayerMoveData
            {
                // �������͂��[���ɐݒ�
                Move = float2.zero
            });
        }
    }

    // ���̃f�[�^�̓v���C���[�̓��̓V�X�e���ɂ���čX�V����܂��B
    public struct PlayerMoveData : IComponentData
    {
        // �v���C���[�̓��͂Ɋ�Â�2D�x�N�g��
        public float2 Move; 
    }

    public struct PlayerStatusData : IComponentData
    {
        // �v���C���[�̈ړ����x
        public float MoveSpeed; 
    }
}
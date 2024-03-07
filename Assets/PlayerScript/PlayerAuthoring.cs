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
        public float2 Move; // �v���C���[�̓��͂Ɋ�Â�2D�x�N�g��
    }

    public struct PlayerStatusData : IComponentData
    {
        public float MoveSpeed; // �v���C���[�̈ړ����x
    }
}


//using Unity.Entities;
//using Unity.Mathematics;
//using UnityEngine;

///// <summary>
///// PlayerAuthoring �R���|�[�l���g�́AGameObject�@��ECS�̃G���e�B�e�B�ɕϊ�
/////�v���C���[�Ɋ֘A����f�[�^�R���|�[�l���g��ǉ����邽�߂Ɏg�p
///// </summary>
//public class PlayerAuthoring : MonoBehaviour
//{
//    // �v���C���[�̈ړ����x
//    [SerializeField]
//    private float moveSpeed;

//    /// PlayerAuthoring �̃f�[�^�����ƂɁA�G���e�B�e�B�Ɂ@PlayerMoveData,PlayerStatusData�@�R���|�[�l���g��ǉ�����Baker�N���X
//    private class Baker : Baker<PlayerAuthoring>
//    {
//        // Bake ���\�b�h�́AAuthoring �R���|�[�l���g����G���e�B�e�B�Ƀf�[�^��ϊ�
//        public override void Bake(PlayerAuthoring authoring)
//        {
//            // ���I�ɓ���GameObject��Entity�ɕϊ����邽�߂̃t���O��ݒ�
//            TransformUsageFlags flags = TransformUsageFlags.Dynamic;
//            // GameObject��Entity�ɕϊ�
//            Entity entity = GetEntity(authoring, flags);

//            // �v���C���[�G���e�B�e�B��PlayerStatusData�R���|�[�l���g��ǉ�
//            AddComponent(entity, new PlayerStatusData
//            {
//                // �ړ����x��ݒ�
//                MoveSpeed = authoring.moveSpeed
//            });

//            // �v���C���[�G���e�B�e�B��PlayerMoveData�R���|�[�l���g��ǉ�
//            AddComponent<PlayerMoveData>(entity);
//        }
//    }

//    /// ���̃f�[�^�̓v���C���[�̓��̓V�X�e���ɂ���čX�V
//    public struct PlayerMoveData : IComponentData
//    {
//        // �v���C���[�̓��͂Ɋ�Â�2D�x�N�g��
//        public float2 Move;
//    }

//    public struct PlayerStatusData : IComponentData
//    {
//        // �v���C���[�̈ړ����x
//        public float MoveSpeed;
//    }
//}
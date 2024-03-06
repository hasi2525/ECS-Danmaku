using Unity.Entities;
using UnityEngine;

public class PlayerTagAuthoring : MonoBehaviour
{
    // Baker �N���X�́AAuthoring �R���|�[�l���g����G���e�B�e�B�Ƀf�[�^���u�Ă����ށv���߂Ɏg�p
    private class Baker : Baker<PlayerTagAuthoring>
    {
        // Bake ���\�b�h�́AAuthoring �R���|�[�l���g����G���e�B�e�B�Ƀf�[�^��ϊ�
        public override void Bake(PlayerTagAuthoring authoring)
        {
            // ���I�ɓ���GameObject��Entity�ɕϊ����邽�߂̃t���O��ݒ�
            TransformUsageFlags flags = TransformUsageFlags.None;
            // GameObject��Entity�ɕϊ�
            Entity entity = GetEntity(authoring, flags);

            // �v���C���[�G���e�B�e�B��PlayerMoveData�R���|�[�l���g���ǉ�
            AddComponent<PlayerTag>(entity);
        }
    }

    public struct PlayerTag : IComponentData
    {
    }
}
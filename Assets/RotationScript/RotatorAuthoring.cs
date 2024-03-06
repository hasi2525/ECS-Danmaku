using UnityEngine;
using Unity.Entities;

/// <summary>
/// GameObject�ɃA�^�b�`���āA�G�f�B�^�[�����]���x��ݒ肷�邽�߂̃R���|�[�l���g
/// ECS�̃G���e�B�e�B�ւ̕ϊ����ɁA���̐ݒ�����Ƃ�RotatorData�R���|�[�l���g���ǉ�
/// </summary>
public class RotatorAuthoring : MonoBehaviour
{
    // ��]���x
    [SerializeField]
    private float speed;

    /// <summary>
    /// RotatorAuthoring �̃f�[�^�����ƂɁA�G���e�B�e�B�Ɂ@RotatorData�@�R���|�[�l���g��ǉ�����Baker�N���X
    /// </summary>
    private class Baker : Baker<RotatorAuthoring>
    {
        /// <summary>
        /// Authoring�R���|�[�l���g����G���e�B�e�B�Ƀf�[�^��ϊ�
        /// </summary>
        /// <param name="authoring">RotatorAuthoring�R���|�[�l���g</param>
        public override void Bake(RotatorAuthoring authoring)
        {
            // ���I�ɓ���GameObject�ł���Ƃ��āAGameObject��Entity�ɕϊ�
            TransformUsageFlags flags = TransformUsageFlags.Dynamic;
            Entity entity = GetEntity(authoring, flags);

            // ��]�f�[�^�����R���|�[�l���g���G���e�B�e�B�ɒǉ�
            AddComponent(entity, new RotatorData
            {
                speed = authoring.speed
            });
        }
    }

    /// <summary>
    /// �G���e�B�e�B����]���鑬�x������ECS�R���|�[�l���g
    /// </summary>
    public struct RotatorData : IComponentData
    {
        public float speed; // ��]���x
    }
}
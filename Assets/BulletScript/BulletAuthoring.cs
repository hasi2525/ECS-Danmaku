using UnityEngine;
using Unity.Entities;

/// <summary>
/// �e�̐ݒ��Unity��Inspector��Őݒ�
/// ECS�̃G���e�B�e�B�ɕK�v�ȃR���|�[�l���g�f�[�^��ǉ�
/// </summary>
public class BulletAuthoring : MonoBehaviour
{
    // �e�̑��x
    [SerializeField]
    private float speed;
    // �e���^����_���[�W
    [SerializeField]
    private int damage;
    // �e�������I�ɔj�󂳂�鋗��
    [SerializeField]
    private float autoDestroyDistance; 

    /// <summary>
    /// BulletAuthoring �̃f�[�^�����ƂɁA�G���e�B�e�B��BulletData�R���|�[�l���g��ǉ�����Baker�N���X
    /// </summary>
    private class Baker : Baker<BulletAuthoring>
    {
        public override void Bake(BulletAuthoring authoring)
        {
            // �e�� GameObject �𓮓I�ȃG���e�B�e�B�ɕϊ�
            Entity entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
            // Entity.Null�`�F�b�N
            if (entity == Entity.Null) return;

            // �e�̃G���e�B�e�B�� BulletData �R���|�[�l���g��ǉ�
            AddComponent(entity, new BulletData
            {
                Speed = authoring.speed,
                Damage = authoring.damage,
                AutoDestroyDistance = authoring.autoDestroyDistance
            });
        }
    }

    /// <summary>
    /// �e�Ɋ֘A����f�[�^��ێ�����ECS�R���|�[�l���g
    /// </summary>
    public struct BulletData : IComponentData
    {
        // �e�̑��x
        public float Speed;
        // �e�ɂ���ė^������_���[�W
        public int Damage;
        // �����j��܂ł̋���
        public float AutoDestroyDistance;
    }
}
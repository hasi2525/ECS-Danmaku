using UnityEngine;
using Unity.Entities;

/// <summary>
/// Enemy�̐ݒ��Unity�̃C���X�y�N�^�[����s���AECS�̃G���e�B�e�B�ɕϊ����邽�߂̃I�[�T�����O�R���|�[�l���g
/// </summary>
public class EnemyAuthoring : MonoBehaviour
{
    // Enemy�̍ő�HP
    [SerializeField]
    private int maxHp; 

    /// <summary>
    /// Enemy�̃f�[�^��ECS�̃R���|�[�l���g�f�[�^�ɕϊ�����Baker�N���X
    /// </summary>
    private class Baker : Baker<EnemyAuthoring>
    {
        /// <summary>
        /// EnemyAuthoring�R���|�[�l���g����Enemy��ECS�G���e�B�e�B�ɕK�v�ȃf�[�^��ϊ����Đݒ肷��
        /// </summary>
        /// <param name="authoring">�ϊ�����EnemyAuthoring�R���|�[�l���g</param>
        public override void Bake(EnemyAuthoring authoring)
        {
            // Enemy��Entity�𓮓I�G���e�B�e�B�Ƃ��Ď擾
            Entity entity = GetEntity(authoring, TransformUsageFlags.Dynamic);

            // EnemyData�R���|�[�l���g���쐬���A�ő�HP�ƌ���HP��ݒ�
            EnemyData enemyData = new EnemyData
            {
                // �ő�HP��ݒ�
                MaxHp = authoring.maxHp,
                // ������Ԃł͍ő�HP�Ɠ���
                CurrentHp = authoring.maxHp,
            };

            // �쐬����EnemyData�R���|�[�l���g���G���e�B�e�B�ɒǉ�
            AddComponent(entity, enemyData);
        }
    }
}
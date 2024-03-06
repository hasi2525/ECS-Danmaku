using UnityEngine;
using Unity.Entities;

/// <summary>
/// Enemy�̐ݒ��Unity�̃C���X�y�N�^�[����s���AECS�̃G���e�B�e�B�ɕϊ����邽�߂̃I�[�T�����O�R���|�[�l���g�B
/// </summary>
public class EnemyAuthoring : MonoBehaviour
{
    [SerializeField]
    private int maxHp; // Enemy�̍ő�HP�B�C���X�y�N�^�[����ݒ�\�B

    /// <summary>
    /// Enemy�̃f�[�^��ECS�̃R���|�[�l���g�f�[�^�ɕϊ�����Baker�N���X�B
    /// </summary>
    private class Baker : Baker<EnemyAuthoring>
    {
        /// <summary>
        /// EnemyAuthoring�R���|�[�l���g����Enemy��ECS�G���e�B�e�B�ɕK�v�ȃf�[�^��ϊ����Đݒ肷��B
        /// </summary>
        /// <param name="authoring">�ϊ�����EnemyAuthoring�R���|�[�l���g�B</param>
        public override void Bake(EnemyAuthoring authoring)
        {
            // Enemy��Entity�𓮓I�G���e�B�e�B�Ƃ��Ď擾
            Entity entity = GetEntity(authoring, TransformUsageFlags.Dynamic);

            // EnemyData�R���|�[�l���g���쐬���A�ő�HP�ƌ���HP��ݒ�
            EnemyData enemyData = new EnemyData
            {
                MaxHp = authoring.maxHp, // �ő�HP��ݒ�
                CurrentHp = authoring.maxHp, // ������Ԃł͍ő�HP�Ɠ���
            };

            // �쐬����EnemyData�R���|�[�l���g���G���e�B�e�B�ɒǉ�
            AddComponent(entity, enemyData);
        }
    }
}
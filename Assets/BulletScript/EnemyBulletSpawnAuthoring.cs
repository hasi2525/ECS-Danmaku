using Unity.Entities;
using UnityEngine;

/// <summary>
/// �v���C���[�̒e���ːݒ���s�����߂̃I�[�T�����O�R���|�[�l���g
/// ���̃R���|�[�l���g��GameObject�ɃA�^�b�`����Unity�G�f�B�^�[����e���˂̃v���p�e�B���`���A
/// ECS�̐��E�ŗ��p���邽�߂ɃG���e�B�e�B�̃f�[�^�Ƃ��ĕϊ�
/// </summary>
public class EnemyBulletSpawnAuthoring : MonoBehaviour
{
    // ���˂���e�̃v���n�u
    [SerializeField]
    private GameObject bulletPrefab;
    // �e�𔭎˂���Ԋu
    [SerializeField]
    private float spawnInterval;

    /// <summary>
    /// �I�[�T�����O�f�[�^��ECS�̃R���|�[�l���g�f�[�^�ɕϊ����邽�߂�Baker�N���X
    /// </summary>
    private class Baker : Baker<EnemyBulletSpawnAuthoring>
    {
        /// <summary>
        /// PlayerBulletSpawnAuthoring�R���|�[�l���g�̃f�[�^��ECS�̃R���|�[�l���g�f�[�^�ɕϊ�
        /// </summary>
        /// <param name="authoring">�ϊ����̃I�[�T�����O�R���|�[�l���g</param>
        public override void Bake(EnemyBulletSpawnAuthoring authoring)
        {
            // �e�̃v���t�@�u��ECS�̃G���e�B�e�B�ɕϊ�
            Entity bulletPrototype = GetEntity(authoring.bulletPrefab, TransformUsageFlags.Dynamic);

            //// ���˂���G���e�B�e�B�p�̃t���O�B���I�ȓ���������
            //TransformUsageFlags flags = TransformUsageFlags.Dynamic;
            // �I�[�T�����O�R���|�[�l���g������GameObject����G���e�B�e�B���擾
            Entity entity = GetEntity(authoring, TransformUsageFlags.Dynamic);

            // �e���ːݒ�����R���|�[�l���g�f�[�^���쐬���A�G���e�B�e�B�ɒǉ�
            EnemyBulletSpawnData playerBulletSpawnData = new EnemyBulletSpawnData
            {
                BulletPrototype = bulletPrototype,
                // �e�𔭎˂���Ԋu
                BulletSpawnInterval = authoring.spawnInterval,
                // ���˂܂ł̌��݂̃^�C�}�[�����Z�b�g
                BulletSpawnCurrentTime = 0f,
            };
            // �G���e�B�e�B��PlayerBulletSpawnData�R���|�[�l���g��ǉ�
            AddComponent(entity, playerBulletSpawnData);
        }
    }
}
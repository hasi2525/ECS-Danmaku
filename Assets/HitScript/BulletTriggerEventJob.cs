using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using static BulletAuthoring;

/// <summary>
/// �e�Ƒ��̃G���e�B�e�B���Փ˂����ۂ̃C�x���g����������W���u
/// �e�ƏՓ˂����G���e�B�e�B�i�G�Ȃǁj�̏������s��
/// </summary>
[BurstCompile]
public struct BulletTriggerEventJob : ITriggerEventsJob
{
    // �G�G���e�B�e�B�̃f�[�^�ɃA�N�Z�X���邽�߂̃R���|�[�l���g���b�N�A�b�v
    [ReadOnly]
    public ComponentLookup<EnemyData> EnemyLookup;

    // �ʒu���ɃA�N�Z�X���邽�߂̃R���|�[�l���g���b�N�A�b�v
    [ReadOnly]
    public ComponentLookup<LocalTransform> LocalTransformLookup;

    // �e�̃f�[�^�ɃA�N�Z�X���邽�߂̃R���|�[�l���g���b�N�A�b�v
    [ReadOnly]
    public ComponentLookup<BulletData> BulletLookup;

    // �G���e�B�e�B�𑀍삷�邽�߂̃G���e�B�e�B�}�l�[�W��
    public EntityManager EntityManager;

    // �G���e�B�e�B�̕ύX���ꊇ�ōs�����߂̃R�}���h�o�b�t�@
    public EntityCommandBuffer EntityCommandBuffer;

    /// <summary>
    /// �g���K�[�C�x���g�i�Փ˃C�x���g�j�����������Ƃ��Ɏ��s
    /// �e�ۂƑ��̃G���e�B�e�B�̏Փˏ������s��
    /// </summary>
    /// <param name="triggerEvent">�Փ˃C�x���g�̃f�[�^</param>
    [BurstCompile]
    public void Execute(TriggerEvent triggerEvent)
    {
        // �Փ˂���2��Entity�����o��
        Entity entityA = triggerEvent.EntityA;
        Entity entityB = triggerEvent.EntityB;

        // �ǂ��炪�e�ۂ��𒲂ׂ�
        bool isBulletA = BulletLookup.HasComponent(entityA);
        bool isBulletB = BulletLookup.HasComponent(entityB);

        // �����܂��͂ǂ�����e�łȂ��ꍇ�͏������I��
        if (isBulletA == isBulletB) return;

        // �e�̃G���e�B�e�B�����
        Entity bulletEntity = isBulletA ? entityA : entityB;

        // �e��j�����܂�
        EntityCommandBuffer.DestroyEntity(bulletEntity);

        // �Փ˂����̂��G�������ꍇ�A�G�̃f�[�^���X�V
        if (EnemyLookup.TryGetComponent(entityA, out EnemyData enemyData))
        {
            // �e�̃f�[�^���擾���A�G��HP�����炷
            BulletData bulletData = BulletLookup[bulletEntity];
            enemyData.CurrentHp -= bulletData.Damage;

            // ��e��Ԃ�Z���Ԉێ�
            enemyData.NoDamageTimeLeft = 0.1f;
            EntityCommandBuffer.SetComponent(entityA, enemyData);

            // HP��0�ȉ��ɂȂ����G�͔j��
            if (enemyData.CurrentHp <= 0)
            {
                EntityCommandBuffer.DestroyEntity(entityA);
            }
        }
    }
}
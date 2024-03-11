using Unity.Entities;
using Unity.Burst;
using Unity.Physics.Systems;
using Unity.Transforms;
using Unity.Physics;
using Unity.Collections;
using static BulletAuthoring;

/// <summary>
/// �e�ۂ����̃G���e�B�e�B�ƏՓ˂����C�x���g����������V�X�e��
/// �Փ˂����e�ۂ�j�����A�ՓˑΏۂɉ��������������s
/// </summary>
[BurstCompile]
[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
// ���������̃V�X�e���̌�Ɏ��s����
[UpdateAfter(typeof(PhysicsSystemGroup))]
public partial struct BulletTriggerEventSystem : ISystem
{
    // �e�R���|�[�l���g�̃f�[�^�ɃA�N�Z�X���邽�߂�Lookup
    private ComponentLookup<LocalTransform> _localTransformLookup;
    private ComponentLookup<BulletData> _bulletLookup;
    private ComponentLookup<EnemyData> _enemyLookup;

    [BurstCompile]
    void ISystem.OnCreate(ref SystemState state)
    {
        //�V�X�e�����X�V����邽�߂ɕK�v�ȃR���|�[�l���g�^�C�v���w��
        state.RequireForUpdate<SimulationSingleton>();
        state.RequireForUpdate<LocalTransform>();
        state.RequireForUpdate<BulletData>();

        // �e�R���|�[�l���g�f�[�^�ւ̎Q�Ƃ��擾
        _localTransformLookup = state.GetComponentLookup<LocalTransform>(true);
        _bulletLookup = state.GetComponentLookup<BulletData>(true);
        _enemyLookup = state.GetComponentLookup<EnemyData>(true);
    }

    [BurstCompile]
    void ISystem.OnUpdate(ref SystemState state)
    {
        // Lookup�̍X�V
        _localTransformLookup.Update(ref state);
        _bulletLookup.Update(ref state);
        _enemyLookup.Update(ref state);

        // �G���e�B�e�B�}�l�[�W���ƃV�~�����[�V�����V���O���g���̎擾
        EntityManager entityManager = state.EntityManager;
        SimulationSingleton simulationSingleton = SystemAPI.GetSingleton<SimulationSingleton>();

        //EntityCommandBuffer���擾
        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer
        (Allocator.TempJob, PlaybackPolicy.MultiPlayback);

        // �Փ˔��菈��
        new BulletTriggerEventJob
        {
            LocalTransformLookup = _localTransformLookup,
            BulletLookup = _bulletLookup,
            EnemyLookup = _enemyLookup,
            EntityManager = entityManager,
            EntityCommandBuffer = entityCommandBuffer
        }
        .Schedule(simulationSingleton, state.Dependency)
        .Complete();

        // �R�}���h�o�b�t�@�̎��s
        entityCommandBuffer.Playback(entityManager);
        entityCommandBuffer.Dispose();
    }
}
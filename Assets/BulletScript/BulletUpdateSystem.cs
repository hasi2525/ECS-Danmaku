using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;
using static BulletAuthoring;

/// <summary>
/// �e�̈ړ��Ǝ����j�����������V�X�e��
/// BulletData�R���|�[�l���g�������ׂẴG���e�B�e�B�ɑ΂��āA
/// �w�肳�ꂽ���x�ňړ������A�ݒ肵�������𒴂���Ǝ����I�ɔj��
/// </summary>
[UpdateInGroup(typeof(SimulationSystemGroup))]
[BurstCompile]
public partial struct BulletUpdateSystem : ISystem
{
    private EntityQuery _entityQuery;

    /// <summary>
    /// �V�X�e���̏��������ɌĂяo����郁�\�b�h�B�K�v�ȃR���|�[�l���g�ŃG���e�B�e�B�N�G�����쐬
    /// </summary>
    [BurstCompile]
    void ISystem.OnCreate(ref SystemState state)
    {
        // �R���|�[�l���g�^�C�v�̔z����쐬���A�N�G���Ώۂ̃R���|�[�l���g���w��
        NativeArray<ComponentType> components = new NativeArray<ComponentType>(2, Allocator.Temp);
        components[0] = ComponentType.ReadWrite<BulletData>();
        components[1] = ComponentType.ReadWrite<LocalTransform>();

        // �G���e�B�e�B�N�G�����쐬
        _entityQuery = state.GetEntityQuery(components);

        // �ꎞ�I�ɍ쐬�����z������
        components.Dispose();
    }
    /// <summary>
    /// �t���[�����ƂɌĂяo����A�e�ۂ̈ړ��Ǝ����j�������
    /// </summary>
    /// 
    [BurstCompile]
    void ISystem.OnUpdate(ref SystemState state)
    {
        // EntityManager��EntityCommandBuffer���擾
        EntityManager entityManager = state.EntityManager;
        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp, PlaybackPolicy.MultiPlayback);

        // Time.DeltaTime���擾
        float deltaTime = SystemAPI.Time.DeltaTime;

        // �N�G���Ƀ}�b�`����G���e�B�e�B�̔z����擾
        NativeArray<Entity> entities = _entityQuery.ToEntityArray(Allocator.Temp);
        foreach (Entity entity in entities)
        {
            // �G���e�B�e�B����BulletData��LocalTransform�R���|�[�l���g���擾
            BulletData bulletData = entityManager.GetComponentData<BulletData>(entity);
            LocalTransform localTransform = entityManager.GetComponentData<LocalTransform>(entity);

            // �e�̈ړ��������v�Z���A�e�̎����j�󋗗����X�V
            float movement = bulletData.Speed * deltaTime;
            bulletData.AutoDestroyDistance -= movement;

            // �e�̐V�����ʒu���v�Z���čX�V
            float3 deltaPosition = localTransform.Forward() * movement;
            localTransform.Position += deltaPosition;

            // �e�̈ړ����ƈʒu�����X�V
            entityManager.SetComponentData(entity, bulletData);
            entityManager.SetComponentData(entity, localTransform);

            // �����j�󋗗���0�ȉ��ɂȂ�����A�G���e�B�e�B��j��
            if (bulletData.AutoDestroyDistance <= 0f)
            {
                entityCommandBuffer.DestroyEntity(entity);
            }
        }

        // �R�}���h�o�b�t�@�����s���A�G���e�B�e�B�̕ύX��K�p
        entityCommandBuffer.Playback(entityManager);

        // �g�p�������\�[�X�����
        entities.Dispose();
        entityCommandBuffer.Dispose();
    }
}
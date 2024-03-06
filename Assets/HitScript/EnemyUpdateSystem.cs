using Unity.Entities;
using Unity.Burst;
using Unity.Mathematics;

/// <summary>
/// �G�̏�Ԃ��X�V����V�X�e��
/// �G���_���[�W���󂯂��ۂ̃q�b�g�L�����Ԃ����������A0�ɂȂ�����q�b�g��Ԃ�����
/// </summary>
[BurstCompile]
public partial struct EnemyUpdateSystem : ISystem
{
    /// <summary>
    /// �V�X�e��������������BEnemyData�R���|�[�l���g�����݂���G���e�B�e�B���A�N�e�B�u�Ȏ��̂ݍX�V����悤�ɐݒ�
    /// </summary>
    [BurstCompile]
    void ISystem.OnCreate(ref SystemState state)
    {
        // �V�X�e���̍X�V�v����ݒ�
        state.RequireForUpdate<EnemyData>();
    }

    /// <summary>
    /// �G�G���e�B�e�B�̏�ԍX�V���s��
    /// �e�t���[�����ƂɓG�̃q�b�g�L�����Ԃ����������A0�ȉ��ɂȂ�����q�b�g��Ԃ����Z�b�g
    /// </summary>
    [BurstCompile]
    void ISystem.OnUpdate(ref SystemState state)
    {
        // �f���^�^�C�����擾
        float deltaTime = SystemAPI.Time.DeltaTime;
        
        // �S�Ă̓G�G���e�B�e�B�ɑ΂��ď��������s
        foreach (var query in SystemAPI.Query<RefRW<EnemyData>>())
        {
            // ���݂̓G�f�[�^���擾
            EnemyData enemyData = query.ValueRO;
            
            // �q�b�g�L�����Ԃ�����������
            enemyData.NoDamageTimeLeft = math.max(enemyData.NoDamageTimeLeft - deltaTime, 0f);

            // �X�V���ꂽ�G�f�[�^��ݒ�
            query.ValueRW = enemyData;
        }
    }
}
using Unity.Mathematics;
using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;
using static PlayerAuthoring;

/// <summary>
/// PlayerUpdateSystem�́A�v���C���[�̈ʒu����͂ƈړ����x�Ɋ�Â��čX�V
/// Burst�R���p�C���𗘗p���č���������ASimulationSystemGroup �ōX�V
/// </summary>
[BurstCompile]
[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial struct PlayerUpdateSystem : ISystem
{
    /// <summary>
    /// �V�X�e�������삷�邽�߂ɕK�v�ȃR���|�[�l���g�f�[�^�̗v����ݒ�
    /// </summary>
    [BurstCompile]
    void ISystem.OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerMoveData>();
        state.RequireForUpdate<PlayerStatusData>();
        state.RequireForUpdate<LocalTransform>();
    }

    /// <summary>
    /// �v���C���[�̓��͂ƈړ����x����ɃG���e�B�e�B�̈ʒu���X�V
    /// SystemAPI.Query���g�p���Ċ֘A����R���|�[�l���g�f�[�^���N�G�����A�v���C���[�̐V�����ʒu���v�Z
    /// </summary>
    [BurstCompile]
    void ISystem.OnUpdate(ref SystemState state)
    {
        // ���݂̃t���[���ƑO�̃t���[���Ƃ̎��ԍ����擾
        float deltaTime = SystemAPI.Time.DeltaTime;

        // �V���O���g���Ƃ��ĕۑ�����Ă���v���C���[�̈ړ����̓f�[�^���擾
        PlayerMoveData playerInputData = SystemAPI.GetSingleton<PlayerMoveData>();

        // �v���C���[�̓��͂Ɋ�Â��Čv�Z���ꂽ�ړ���
        float3 movement = new float3(playerInputData.Move.x, 0, playerInputData.Move.y) * deltaTime;

        // �v���C���[�̈ړ����x�f�[�^ (PlayerStatusData) �ƈʒu��� (LocalTransform) �������ׂẴG���e�B�e�B�ɑ΂��ă��[�v
        foreach ((RefRO<PlayerStatusData> playerParam, RefRW<LocalTransform> localTransform) in SystemAPI.Query<RefRO<PlayerStatusData>, RefRW<LocalTransform>>())
        {
            // �v���C���[�̈ړ����x���擾
            float moveSpeed = playerParam.ValueRO.MoveSpeed;
            // �V�����ʒu���v�Z���ALocalTransform �R���|�[�l���g�ɐݒ�
            localTransform.ValueRW.Position += movement * moveSpeed;
        }
    }
}
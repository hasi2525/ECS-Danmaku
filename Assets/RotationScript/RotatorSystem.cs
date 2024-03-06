using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static RotatorAuthoring;

/// <summary>
/// �G���e�B�e�B����]������V�X�e��
/// RotatorData�R���|�[�l���g�������ׂẴG���e�B�e�B�ɑ΂��āA�w�肳�ꂽ���x�ŉ�]��K�p
/// </summary>
public partial class RotatorSystem : SystemBase
{
    /// <summary>
    /// �t���[�����ƂɃG���e�B�e�B�̉�]���X�V
    /// </summary>
    protected override void OnUpdate()
    {
        // �f���^�^�C�����擾
        float deltaTime = SystemAPI.Time.DeltaTime;

        // Entities.ForEach���g���āA���ׂẴG���e�B�e�B�����[�v���܂��B
        Entities
            .ForEach((ref LocalTransform rotation, in RotatorData rotator) =>
            {
                // �G���e�B�e�B�̉�]���X�V , Y����rotator.speed * deltaTime�̑��x�ŉ�]
                rotation.Rotation = math.mul(
                    math.normalize(rotation.Rotation), 
                    quaternion.Euler(0f, math.radians(rotator.speed * deltaTime), 0f)); 
            })
           // ����ɃX�P�W���[��
           .ScheduleParallel(); 
    }
}
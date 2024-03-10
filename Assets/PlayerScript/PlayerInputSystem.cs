//using Unity.Entities;
//using Unity.Jobs;
//using UnityEngine;
//using UnityEngine.InputSystem;
//using static PlayerAuthoring;

//[UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)]
//public partial class PlayerInputSystem : SystemBase
//{
//    private InputAction _moveInputAction;

//    protected override void OnCreate()
//    {
//        _moveInputAction = new InputAction("Move");
//        _moveInputAction.AddCompositeBinding("Dpad")
//            .With("Up", "<Keyboard>/w")
//            .With("Down", "<Keyboard>/s")
//            .With("Left", "<Keyboard>/a")
//            .With("Right", "<Keyboard>/d");
//        _moveInputAction.Enable();

//        // �R�[���o�b�N���g�p���ē��͂�����
//        _moveInputAction.performed += ctx => UpdateMoveInput(ctx.ReadValue<Vector2>());
//        _moveInputAction.canceled += ctx => UpdateMoveInput(Vector2.zero);
//    }

//    private void UpdateMoveInput(Vector2 input)
//    {
//        var moveData = new PlayerMoveData { Move = input };
//        // �V���O���g���R���|�[�l���g�Ƃ��ăv���C���[�̓��͂�ݒ�
//        EntityManager.SetComponentData(SystemAPI.GetSingletonEntity<PlayerMoveData>(), moveData);
//    }

//    protected override void OnDestroy()
//    {
//        _moveInputAction.Disable();
//        _moveInputAction.Dispose();
//    }

//    protected override void OnUpdate()
//    {
//        // ���̗�ł́A���͂̓R�[���o�b�N��ʂ��ď�������邽�߁AOnUpdate�ł͉������܂���B
//    }
//}


using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerAuthoring;

/// <summary>
/// PlayerInputSystem�́A�v���C���[�̈ړ����͂��擾�APlayerMoveData�R���|�[�l���g�Ɋi�[
/// </summary>

[UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)]
public partial class PlayerInputSystem : SystemBase
{
    // �v���C���[�̈ړ����͂��Ǘ�����InputAction
    private InputAction _moveInputAction;

    /// <summary>
    /// �V�X�e���̏��������ɌĂяo����܂��BInputAction��ݒ肵�ėL����
    /// </summary>
    protected override void OnCreate()
    {
        // �L�[�{�[�h�̓��͂�ݒ�
        _moveInputAction = new InputAction("Move"/*, binding: "<Gamepad>/leftStick"*/);
        _moveInputAction.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
        _moveInputAction.Enable();
    }

    /// <summary>
    /// �V�X�e���̔j�����ɌĂяo����AInputAction�𖳌������j��
    /// </summary>
    protected override void OnDestroy()
    {
        _moveInputAction.Disable();
        _moveInputAction.Dispose();
    }

    /// <summary>
    /// �t���[�����ƂɌĂяo����A�v���C���[�̓��͂Ɋ�Â���PlayerMoveData���X�V
    /// </summary>
    protected override void OnUpdate()
    {
        // ���͒l�̎擾
        Vector2 moveInput = _moveInputAction.ReadValue<Vector2>();

        JobHandle inputJobHandle = Entities.ForEach((ref PlayerMoveData moveData) =>
        {
            // ���͒l��PlayerMoveData�ɐݒ�
            moveData.Move = moveInput;

            // ����W���u�Ƃ��ăX�P�W���[��
        }).ScheduleParallel(Dependency);

        // �W���u�̊�����҂�
        inputJobHandle.Complete();

        // �ˑ��֌W���X�V
        Dependency = inputJobHandle;
    }
}
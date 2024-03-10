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

//        // コールバックを使用して入力を処理
//        _moveInputAction.performed += ctx => UpdateMoveInput(ctx.ReadValue<Vector2>());
//        _moveInputAction.canceled += ctx => UpdateMoveInput(Vector2.zero);
//    }

//    private void UpdateMoveInput(Vector2 input)
//    {
//        var moveData = new PlayerMoveData { Move = input };
//        // シングルトンコンポーネントとしてプレイヤーの入力を設定
//        EntityManager.SetComponentData(SystemAPI.GetSingletonEntity<PlayerMoveData>(), moveData);
//    }

//    protected override void OnDestroy()
//    {
//        _moveInputAction.Disable();
//        _moveInputAction.Dispose();
//    }

//    protected override void OnUpdate()
//    {
//        // この例では、入力はコールバックを通じて処理されるため、OnUpdateでは何もしません。
//    }
//}


using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerAuthoring;

/// <summary>
/// PlayerInputSystemは、プレイヤーの移動入力を取得、PlayerMoveDataコンポーネントに格納
/// </summary>

[UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)]
public partial class PlayerInputSystem : SystemBase
{
    // プレイヤーの移動入力を管理するInputAction
    private InputAction _moveInputAction;

    /// <summary>
    /// システムの初期化時に呼び出されます。InputActionを設定して有効化
    /// </summary>
    protected override void OnCreate()
    {
        // キーボードの入力を設定
        _moveInputAction = new InputAction("Move"/*, binding: "<Gamepad>/leftStick"*/);
        _moveInputAction.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
        _moveInputAction.Enable();
    }

    /// <summary>
    /// システムの破棄時に呼び出され、InputActionを無効化し破棄
    /// </summary>
    protected override void OnDestroy()
    {
        _moveInputAction.Disable();
        _moveInputAction.Dispose();
    }

    /// <summary>
    /// フレームごとに呼び出され、プレイヤーの入力に基づいてPlayerMoveDataを更新
    /// </summary>
    protected override void OnUpdate()
    {
        // 入力値の取得
        Vector2 moveInput = _moveInputAction.ReadValue<Vector2>();

        JobHandle inputJobHandle = Entities.ForEach((ref PlayerMoveData moveData) =>
        {
            // 入力値をPlayerMoveDataに設定
            moveData.Move = moveInput;

            // 並列ジョブとしてスケジュール
        }).ScheduleParallel(Dependency);

        // ジョブの完了を待つ
        inputJobHandle.Complete();

        // 依存関係を更新
        Dependency = inputJobHandle;
    }
}
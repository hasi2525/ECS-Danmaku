using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// PlayerAuthoring コンポーネントは、GameObjectをECSのエンティティに変換し、
/// プレイヤーに関連するデータコンポーネントを追加するために使用します。
/// </summary>
public class PlayerAuthoring : MonoBehaviour
{
    // プレイヤーの移動速度
    [SerializeField]
    private float moveSpeed = 5.0f;

    private class Baker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            TransformUsageFlags flags = TransformUsageFlags.Dynamic;
            Entity entity = GetEntity(authoring, flags);

            // プレイヤーエンティティにPlayerStatusDataコンポーネントを追加
            AddComponent(entity, new PlayerStatusData
            {
                MoveSpeed = authoring.moveSpeed
            });

            // プレイヤーエンティティにPlayerMoveDataコンポーネントを追加し、初期値を設定
            AddComponent(entity, new PlayerMoveData
            {
                // 初期入力をゼロに設定
                Move = float2.zero
            });
        }
    }

    // このデータはプレイヤーの入力システムによって更新されます。
    public struct PlayerMoveData : IComponentData
    {
        public float2 Move; // プレイヤーの入力に基づく2Dベクトル
    }

    public struct PlayerStatusData : IComponentData
    {
        public float MoveSpeed; // プレイヤーの移動速度
    }
}


//using Unity.Entities;
//using Unity.Mathematics;
//using UnityEngine;

///// <summary>
///// PlayerAuthoring コンポーネントは、GameObject　をECSのエンティティに変換
/////プレイヤーに関連するデータコンポーネントを追加するために使用
///// </summary>
//public class PlayerAuthoring : MonoBehaviour
//{
//    // プレイヤーの移動速度
//    [SerializeField]
//    private float moveSpeed;

//    /// PlayerAuthoring のデータをもとに、エンティティに　PlayerMoveData,PlayerStatusData　コンポーネントを追加するBakerクラス
//    private class Baker : Baker<PlayerAuthoring>
//    {
//        // Bake メソッドは、Authoring コンポーネントからエンティティにデータを変換
//        public override void Bake(PlayerAuthoring authoring)
//        {
//            // 動的に動くGameObjectをEntityに変換するためのフラグを設定
//            TransformUsageFlags flags = TransformUsageFlags.Dynamic;
//            // GameObjectをEntityに変換
//            Entity entity = GetEntity(authoring, flags);

//            // プレイヤーエンティティにPlayerStatusDataコンポーネントを追加
//            AddComponent(entity, new PlayerStatusData
//            {
//                // 移動速度を設定
//                MoveSpeed = authoring.moveSpeed
//            });

//            // プレイヤーエンティティにPlayerMoveDataコンポーネントを追加
//            AddComponent<PlayerMoveData>(entity);
//        }
//    }

//    /// このデータはプレイヤーの入力システムによって更新
//    public struct PlayerMoveData : IComponentData
//    {
//        // プレイヤーの入力に基づく2Dベクトル
//        public float2 Move;
//    }

//    public struct PlayerStatusData : IComponentData
//    {
//        // プレイヤーの移動速度
//        public float MoveSpeed;
//    }
//}
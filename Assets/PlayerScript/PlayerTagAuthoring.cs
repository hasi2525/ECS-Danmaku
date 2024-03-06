using Unity.Entities;
using UnityEngine;

public class PlayerTagAuthoring : MonoBehaviour
{
    // Baker クラスは、Authoring コンポーネントからエンティティにデータを「焼き込む」ために使用
    private class Baker : Baker<PlayerTagAuthoring>
    {
        // Bake メソッドは、Authoring コンポーネントからエンティティにデータを変換
        public override void Bake(PlayerTagAuthoring authoring)
        {
            // 動的に動くGameObjectをEntityに変換するためのフラグを設定
            TransformUsageFlags flags = TransformUsageFlags.None;
            // GameObjectをEntityに変換
            Entity entity = GetEntity(authoring, flags);

            // プレイヤーエンティティにPlayerMoveDataコンポーネントも追加
            AddComponent<PlayerTag>(entity);
        }
    }

    public struct PlayerTag : IComponentData
    {
    }
}
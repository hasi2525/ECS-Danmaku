using UnityEngine;
using Unity.Entities;

/// <summary>
/// GameObjectにアタッチして、エディターから回転速度を設定するためのコンポーネント
/// ECSのエンティティへの変換時に、この設定をもとにRotatorDataコンポーネントが追加
/// </summary>
public class RotatorAuthoring : MonoBehaviour
{
    // 回転速度
    [SerializeField]
    private float speed;

    /// <summary>
    /// RotatorAuthoring のデータをもとに、エンティティに　RotatorData　コンポーネントを追加するBakerクラス
    /// </summary>
    private class Baker : Baker<RotatorAuthoring>
    {
        /// <summary>
        /// Authoringコンポーネントからエンティティにデータを変換
        /// </summary>
        /// <param name="authoring">RotatorAuthoringコンポーネント</param>
        public override void Bake(RotatorAuthoring authoring)
        {
            // 動的に動くGameObjectであるとして、GameObjectをEntityに変換
            TransformUsageFlags flags = TransformUsageFlags.Dynamic;
            Entity entity = GetEntity(authoring, flags);

            // 回転データを持つコンポーネントをエンティティに追加
            AddComponent(entity, new RotatorData
            {
                speed = authoring.speed
            });
        }
    }

    /// <summary>
    /// エンティティが回転する速度を持つECSコンポーネント
    /// </summary>
    public struct RotatorData : IComponentData
    {
        public float speed; // 回転速度
    }
}
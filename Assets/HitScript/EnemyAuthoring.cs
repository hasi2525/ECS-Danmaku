using UnityEngine;
using Unity.Entities;

/// <summary>
/// Enemyの設定をUnityのインスペクターから行い、ECSのエンティティに変換するためのオーサリングコンポーネント
/// </summary>
public class EnemyAuthoring : MonoBehaviour
{
    // Enemyの最大HP
    [SerializeField]
    private int maxHp; 

    /// <summary>
    /// EnemyのデータをECSのコンポーネントデータに変換するBakerクラス
    /// </summary>
    private class Baker : Baker<EnemyAuthoring>
    {
        /// <summary>
        /// EnemyAuthoringコンポーネントからEnemyのECSエンティティに必要なデータを変換して設定する
        /// </summary>
        /// <param name="authoring">変換元のEnemyAuthoringコンポーネント</param>
        public override void Bake(EnemyAuthoring authoring)
        {
            // EnemyのEntityを動的エンティティとして取得
            Entity entity = GetEntity(authoring, TransformUsageFlags.Dynamic);

            // EnemyDataコンポーネントを作成し、最大HPと現在HPを設定
            EnemyData enemyData = new EnemyData
            {
                // 最大HPを設定
                MaxHp = authoring.maxHp,
                // 初期状態では最大HPと同じ
                CurrentHp = authoring.maxHp,
            };

            // 作成したEnemyDataコンポーネントをエンティティに追加
            AddComponent(entity, enemyData);
        }
    }
}
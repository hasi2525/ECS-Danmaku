using Unity.Entities;

/// <summary>
/// 敵エンティティの基本データを保持するコンポーネント
/// 敵の最大HP、現在のHP、ヒット有効残り時間を含む
/// </summary>
public struct EnemyData : IComponentData
{
    // 敵の最大HP
    public int MaxHp;
    // 敵の現在のHP
    public int CurrentHp;
    // ヒットが有効である残り時間
    public float NoDamageTimeLeft; 

    ///// <summary>
    ///// 敵が現在ヒットに有効かどうかを示すプロパティ
    ///// ヒット有効時間が0より大きい場合はtrueとなり、敵は無敵状態となる
    ///// </summary>
    //public bool IsNoDamage => NoDamageTimeLeft > 0f;
}
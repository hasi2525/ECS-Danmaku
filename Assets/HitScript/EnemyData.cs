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
}
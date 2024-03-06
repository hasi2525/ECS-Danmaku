using Unity.Entities;

// プレイヤーが弾を生成するためのデータを持つコンポーネント
public struct PlayerBulletSpawnData : IComponentData
{
    // 弾のプロトタイプとなるエンティティ
    public Entity BulletPrototype;
    // 弾を生成する間隔
    public float BulletSpawnInterval;
    // 次に弾を生成するまでの現在の時間 
    public float BulletSpawnCurrentTime;
}
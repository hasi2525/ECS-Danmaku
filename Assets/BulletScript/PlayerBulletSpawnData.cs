using Unity.Entities;

// �v���C���[���e�𐶐����邽�߂̃f�[�^�����R���|�[�l���g
public struct PlayerBulletSpawnData : IComponentData
{
    // �e�̃v���g�^�C�v�ƂȂ�G���e�B�e�B
    public Entity BulletPrototype;
    // �e�𐶐�����Ԋu
    public float BulletSpawnInterval;
    // ���ɒe�𐶐�����܂ł̌��݂̎��� 
    public float BulletSpawnCurrentTime;
}
using Unity.Entities;

/// <summary>
/// �G�G���e�B�e�B�̊�{�f�[�^��ێ�����R���|�[�l���g
/// �G�̍ő�HP�A���݂�HP�A�q�b�g�L���c�莞�Ԃ��܂�
/// </summary>
public struct EnemyData : IComponentData
{
    // �G�̍ő�HP
    public int MaxHp;
    // �G�̌��݂�HP
    public int CurrentHp;
    // �q�b�g���L���ł���c�莞��
    public float NoDamageTimeLeft; 
}
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

    ///// <summary>
    ///// �G�����݃q�b�g�ɗL�����ǂ����������v���p�e�B
    ///// �q�b�g�L�����Ԃ�0���傫���ꍇ��true�ƂȂ�A�G�͖��G��ԂƂȂ�
    ///// </summary>
    //public bool IsNoDamage => NoDamageTimeLeft > 0f;
}
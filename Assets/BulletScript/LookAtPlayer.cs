using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform playerTransform; // �v���C���[��Transform

    void Update()
    {
        if (playerTransform != null)
        {
            // �v���C���[�̕���������
            transform.LookAt(playerTransform);
        }
    }
}
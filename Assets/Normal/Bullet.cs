using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f; // �e�ۂ̎����i�b�j

    void Start()
    {
        Destroy(gameObject, lifetime); // lifetime�b��ɒe�ۂ�j��
    }
}
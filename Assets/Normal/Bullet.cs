using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f; // ’eŠÛ‚Ìõ–½i•bj

    void Start()
    {
        Destroy(gameObject, lifetime); // lifetime•bŒã‚É’eŠÛ‚ğ”jŠü
    }
}
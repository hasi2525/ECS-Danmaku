using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    public GameObject bulletPrefab; // �e�ۂ̃v���n�u
    public Transform shootPoint; // �e�ۂ𔭎˂���ʒu
    public float bulletSpeed = 20f; // �e�ۂ̑��x
    public float shootInterval = 0.5f; // ���ˊԊu�i�b�j
    private float lastShootTime; // �Ō�ɒe�ۂ�����������

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time - lastShootTime > shootInterval)
        {
            ShootBullet();
            lastShootTime = Time.time; // �Ō�ɔ��˂����������X�V
        }
    }

    void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * bulletSpeed;
        }
    }
}
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    public GameObject bulletPrefab; // 弾丸のプレハブ
    public Transform shootPoint; // 弾丸を発射する位置
    public float bulletSpeed = 20f; // 弾丸の速度
    public float shootInterval = 0.5f; // 発射間隔（秒）
    private float lastShootTime; // 最後に弾丸を撃った時刻

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time - lastShootTime > shootInterval)
        {
            ShootBullet();
            lastShootTime = Time.time; // 最後に発射した時刻を更新
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
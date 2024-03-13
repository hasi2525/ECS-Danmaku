using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform playerTransform; // プレイヤーのTransform

    void Update()
    {
        if (playerTransform != null)
        {
            // プレイヤーの方向を向く
            transform.LookAt(playerTransform);
        }
    }
}
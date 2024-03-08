using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotor : MonoBehaviour
{
    // 回転速度
    public float rotateSpeed = 50f;
    void Update()
    {
        // 敵キャラクターを回転させる
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotor : MonoBehaviour
{
    // ��]���x
    public float rotateSpeed = 50f;
    void Update()
    {
        // �G�L�����N�^�[����]������
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
}

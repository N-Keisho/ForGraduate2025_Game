using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCam : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f; // 回転速度
    [SerializeField] private Camera cam;        // カメラ
    void Update()
    {
        // カメラを回転させる
        cam.transform.Rotate(Vector3.up, speed * Time.deltaTime);    
    }
}

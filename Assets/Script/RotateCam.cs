using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCam : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;   // 回転速度
    [SerializeField] private Camera cam;            // カメラ
    static private Quaternion camRot;               // カメラの回転

    void Start()
    {
        cam.transform.rotation = camRot;            // カメラの回転を設定
    }

    void Update()
    {
        // カメラを回転させる
        cam.transform.Rotate(Vector3.up, speed * Time.deltaTime); 
        camRot = cam.transform.rotation;            // カメラの回転を取得   
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMove : MonoBehaviour
{
    enum MoveDirection
    {
        LeftRight,
        UpDown
    }
    [SerializeField] private MoveDirection moveDirection = MoveDirection.LeftRight; // 移動方向
    [SerializeField] private float speed = 5f; // 移動速度
    [SerializeField] private float moveDistance = 5f; // 移動距離
    private  Vector3 origin; // 初期位置
    private Vector3 movepos; // 移動位置

    void Start()
    {
        origin = transform.localPosition; // 初期位置を取得
    }
    void Update()
    {
        movepos = Vector3.zero; // 移動位置を初期化
        switch (moveDirection)
        {
            case MoveDirection.LeftRight:
                movepos.x = Mathf.Sin(Time.time * speed) * moveDistance; // 左右に移動
                break;
            case MoveDirection.UpDown:
                movepos.y = Mathf.Sin(Time.time * speed) * moveDistance; // 上下に移動
                break;
        }
        transform.localPosition = origin + movepos; // 移動
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyTransition;
public class Title : MonoBehaviour
{
    [SerializeField] private TransitionSettings transition;
    [SerializeField] private float lodadDelay = 1.0f; // ロード遅延時間
    [SerializeField] private string sceneName = "Game"; // ロードするシーン名
    [SerializeField] private AudioClip start; // 音声クリップ
    [SerializeField] private GameObject logo; // タイトルオブジェクト
    private AudioSource audioSource; // AudioSourceコンポーネント
    private bool isLoading = false; // シーンがロード中かどうか


    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSourceコンポーネントを取得
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isLoading) // 任意のキーが押されたら
        {
            isLoading = true; // ロード中フラグを立てる
            LoadScene(); // シーンをロードする
        }

        logo.transform.localPosition = new Vector3(0, Mathf.Sin(Time.time * 2) * 5, 0); // タイトルの位置を上下に動かす
        
    }
    public void LoadScene()
    {
        audioSource.PlayOneShot(start); // 音声を再生
        TransitionManager.Instance().Transition(sceneName, transition, lodadDelay);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadMain : MonoBehaviour
{
    [SerializeField] private float value;            // 残り時間
    [SerializeField] private float timeLimit;           // 1問当たりの制限時間
    [SerializeField] private float timer;               // タイマー
    [SerializeField] private float period = 1.0f;       // タイマーの周期
    [SerializeField] private Image outGuage;            // 外側のゲージ
    public float maxValue;                              // 最大時間（問題数×制限時間）
    private int questionCount = 19;                     // 問題数（19問くらいが丁度よさそう）
    void Start()
    {
        maxValue =  questionCount * timeLimit;
        timer = 0.0f;
        value = 0.0f;
        outGuage.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= period)
        {
            timer = 0.0f;
            value += 1 / maxValue;
            if(value < 1.0f){
                outGuage.fillAmount = value; // ゲージの更新
            }
            else if(value >= 1.2f)
            {
                outGuage.fillAmount = 1.0f; // ゲージの更新
                SceneManager.LoadScene("Main");
            }
        }
    }
}

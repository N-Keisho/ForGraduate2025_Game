using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EasyTransition;

public class Result : MonoBehaviour
{
    [SerializeField] private TMP_Text correctNumText;   // 正解数を表示するテキスト
    [SerializeField] private TMP_Text allNumText;       // 問題数を表示するテキスト
    [SerializeField] private TMP_Text seikaiText;
    [SerializeField] Image outGuage;                    // 外側のゲージ
    [SerializeField] private AudioClip charge;
    [SerializeField] private AudioClip cymbal;
    [SerializeField] private AudioClip low;
    [SerializeField] private AudioClip mid;
    [SerializeField] private AudioClip high;
    [SerializeField] private TransitionSettings transition;
    [SerializeField] private string sceneName = "Title";
    [SerializeField] private GameObject buttonText;
    [SerializeField] private ParticleSystem leftParticle;
    [SerializeField] private ParticleSystem rightParticle;
    [SerializeField] private ParticleSystem backParticle;
    private float timer = 0.0f;                  // タイマー
    private float period = 0.1f;                // タイマーの周期
    private float value = 0.0f;                // ゲージの値
    private float addValue;                // ゲージの増加量
    private float maxValue;             // ゲージの最大値
    private int preNum = 0;
    private bool isFin =false;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(charge);

        leftParticle.Stop();
        rightParticle.Stop();
        backParticle.Stop();

        allNumText.text = GlobalVariables.questsCount.ToString() + "問中...";
        seikaiText.text = "";

        maxValue = GlobalVariables.correctAnswerCount / (float)GlobalVariables.questsCount;
        addValue = maxValue / (3.0f / period);
        outGuage.fillAmount = 0.0f;

        buttonText.SetActive(false);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= period)
        {
            timer = 0.0f;
            value += addValue;
            if (value < maxValue)
            {
                outGuage.fillAmount = value;
                correctNumText.text = RandomNum().ToString();
            }
            else if(!isFin)
            {
                Fin();
            }
        }

        if(isFin && Input.GetKeyDown(KeyCode.Space))
        {
            GlobalVariables.correctAnswerCount = 0;
            TransitionManager.Instance().Transition(sceneName, transition, 0f);
        }
    }

    int RandomNum()
    {
        int num = preNum;
        while (num == preNum){
            num = Random.Range(0, GlobalVariables.questsCount + 1); // 正解数をランダムに取得
        }
        preNum = num;
        return num;
    }

    void Fin(){
        isFin = true;
        
        outGuage.fillAmount = maxValue;
        
        correctNumText.text = GlobalVariables.correctAnswerCount.ToString();
        seikaiText.text = "せいかい！";
        buttonText.SetActive(true);

        leftParticle.Play();
        rightParticle.Play();
        backParticle.Play();

        audioSource.Stop();
        audioSource.volume = 0.25f;
        audioSource.PlayOneShot(cymbal);
        audioSource.PlayDelayed(1.5f);

        if(GlobalVariables.correctAnswerCount >= (float)GlobalVariables.questsCount / 4 * 3){
            Debug.Log("high");
            audioSource.PlayOneShot(high);
        }
        else if(GlobalVariables.correctAnswerCount >= (float)GlobalVariables.questsCount / 2){
            Debug.Log("mid");
            audioSource.PlayOneShot(mid);
        }
        else{
            Debug.Log("low");
            audioSource.PlayOneShot(low);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guage : MonoBehaviour
{
    [SerializeField] private float leftTime;            // 残り時間
    [SerializeField] private float timeLimit;           // 1問当たりの制限時間
    [SerializeField] private float timer;               // タイマー
    [SerializeField] private float period = 1.0f;       // タイマーの周期
    [SerializeField] private Image inGuage;             // 内側のゲージ
    [SerializeField] private Image outGuage;            // 外側のゲージ
    [SerializeField] private Image nextOutGuage;        // 次の問題の外側のゲージ
    private GameManager gm;                             // ゲームマネージャ
    private Sound sound;                                // 音声再生
    private float maxTime;                              // 最大時間（問題数×制限時間）
    private bool isDenger = false;                     // 警告音が鳴ったかどうか
    private Color addColor;
    void Start()
    {
        addColor = (new Color(1.0f, 1.0f, 0.0f, 1.0f) - outGuage.color) / (timeLimit / period);
        gm = GetComponent<GameManager>();
        sound = GetComponent<Sound>();

        maxTime = GlobalVariables.questsCount * timeLimit;
        timer = 0.0f;
        leftTime = maxTime;
        inGuage.fillAmount = 0f;
        outGuage.fillAmount = 1.0f;
        nextOutGuage.fillAmount = 1f - timeLimit / maxTime;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= period)
        {
            timer = 0.0f;
            leftTime -= period;
            if (leftTime < 0.0f)
            {
                leftTime = 0.0f;
                inGuage.fillAmount = 1.0f;
            }


            outGuage.fillAmount = leftTime / maxTime;
            outGuage.color += addColor;

            if(leftTime < 6.0f && !isDenger && !gm.isCorrect)
            {
                isDenger = true;
                sound.PlayDenger();
            }

            // 制限時間1秒前
            if((leftTime - 1) % timeLimit == 0 && !gm.isCorrect)
            {
                // 答えの表示
                gm.ShowAnswer();
                sound.PlayWrongAnswer();
            }

            // 制限時間経過時
            if (leftTime % timeLimit == 0)
            {
                if (gm.currentQuestIndex < GlobalVariables.questsCount - 1)
                {   
                    gm.NextQuest();
                    outGuage.color = new Color(0.0f, 1.0f, 0.0f, 1.0f); // 緑色
                    inGuage.fillAmount = (float)gm.currentQuestIndex / (float)GlobalVariables.questsCount;
                    nextOutGuage.fillAmount = 1f - (float)(gm.currentQuestIndex + 1) / (float)GlobalVariables.questsCount;
                }
                else
                {
                    gm.NextQuest();
                    inGuage.fillAmount = 1.0f;
                }
            }
        }
    }
}

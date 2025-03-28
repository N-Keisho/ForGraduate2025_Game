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
    private GameManager gm;                             // ゲームマネージャ
    private float maxTime;                              // 最大時間（問題数×制限時間）
    void Start()
    {
        gm = GetComponent<GameManager>();

        maxTime = gm.quests.Count * timeLimit;
        timer = 0.0f;
        leftTime = maxTime;
        inGuage.fillAmount = 0f;
        outGuage.fillAmount = 1.0f;
    }

    // Update is called once per frame
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

            if (leftTime % timeLimit == 0)
            {
                if (gm.currentQuestIndex < gm.quests.Count - 1)
                {   
                    gm.NextQuest();
                    inGuage.fillAmount = (float)gm.currentQuestIndex / (float)gm.quests.Count;
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

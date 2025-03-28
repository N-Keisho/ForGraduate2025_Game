using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public List<Quest> quests;          // 問題のリスト
    public int answer = -1;              // 回答
    public int answerIndex = -1;         // 回答の種類
    public int currentQuestIndex = 0;   // 現在の問題のインデックス
    private int preAnswer = -1;         // 前回の回答
    private int preAnswerIndex = -1;    // 前回の回答の種類
    [SerializeField] private TMP_Text scentence; // 問題文を表示するテキスト
    
    void Start()
    {
        scentence.text = "Q : " + quests[currentQuestIndex].question;
    }

    void Update()
    {
        if(preAnswer != answer || preAnswerIndex != answerIndex)
        {
            if (quests[currentQuestIndex].type == QuestType.Open)
            {
                if (answer == currentQuestIndex)
                {
                    Debug.Log("Correct");
                    NextQuest();
                }
            }
            else if (quests[currentQuestIndex].type == QuestType.Select)
            {
                if (answer == currentQuestIndex && answerIndex == quests[currentQuestIndex].correctIndex)
                {
                    Debug.Log("Correct");
                    NextQuest();
                }
            }
            else if (quests[currentQuestIndex].type == QuestType.Silhouette)
            {
                if (answer == currentQuestIndex)
                {
                    Debug.Log("Correct");
                    NextQuest();
                }
            }
            preAnswer = answer;
            preAnswerIndex = answerIndex;
        }
    }

    void NextQuest()
    {
        if (currentQuestIndex < quests.Count - 1)
        {
            currentQuestIndex++;
            scentence.text = "Q : " + quests[currentQuestIndex].question;
        }
    }
}

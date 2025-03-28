using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
public class GameManager : MonoBehaviour
{
    public List<Quest> quests;          // 問題のリスト
    public int answer = -1;              // 回答
    public int answerIndex = -1;         // 回答の種類
    public int currentQuestIndex = 0;   // 現在の問題のインデックス
    public bool isCorrect = false;     // 回答が正解かどうか
    private int preAnswer = -1;         // 前回の回答
    private int preAnswerIndex = -1;    // 前回の回答の種類
    private int correctAnswerNum = 0;   // 正解数
    private Sound sound;                // 音声再生
    [SerializeField] private TMP_Text scentence; // 問題文を表示するテキスト
    [SerializeField] private TMP_Text answerText; // 回答を表示するテキスト
    [SerializeField] private TMP_Text correctAnswerNumText; // 正解数を表示するテキスト

    void Start()
    {
        sound = GetComponent<Sound>();
        scentence.text = quests[currentQuestIndex].question;
        answerText.text = "";
    }

    void Update()
    {
        if (preAnswer != answer || preAnswerIndex != answerIndex)
        {
            if (quests[currentQuestIndex].type == QuestType.Open)
            {
                if (answer == currentQuestIndex)
                {
                    CorrectAnswer();
                }
            }
            else if (quests[currentQuestIndex].type == QuestType.Select)
            {
                if (answer == currentQuestIndex && answerIndex == quests[currentQuestIndex].correctIndex)
                {
                    CorrectAnswer();
                }
            }
            else if (quests[currentQuestIndex].type == QuestType.Silhouette)
            {
                if (answer == currentQuestIndex)
                {
                    CorrectAnswer();
                }
            }
            preAnswer = answer;
            preAnswerIndex = answerIndex;
        }

        if (Input.GetKey(KeyCode.Space) && Input.GetKeyDown(KeyCode.Return))
        {
            CorrectAnswer();
        }
        else if (Input.anyKeyDown && !isCorrect && !Input.GetKey(KeyCode.Space))
        {
            sound.PlayWrongAnswer();
        }
    }

    void CorrectAnswer()
    {
        if (!isCorrect)
        {
            isCorrect = true;
            correctAnswerNum++;
            correctAnswerNumText.text = correctAnswerNum.ToString();
            ShowAnswer();
            sound.PlayCorrectAnswer();
        }
    }

    public void ResetAnser()
    {
        answer = -1;
        answerIndex = -1;
        answerText.text = "";
        isCorrect = false;
    }

    public void ShowAnswer()
    {
        answerText.text = Regex.Unescape(quests[currentQuestIndex].answerText);
    }

    public void NextQuest()
    {
        if (currentQuestIndex < quests.Count - 1)
        {
            ResetAnser();
            currentQuestIndex++;
            scentence.text = quests[currentQuestIndex].question;
        }
        else
        {
            Debug.Log("Finish");
        }
    }
}

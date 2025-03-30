using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using EasyTransition;
public class GameManager : MonoBehaviour
{
    public List<Quest> quests;          // 問題のリスト
    public int answer = -1;             // 回答
    public int answerIndex = -1;        // 回答の種類
    public int currentQuestIndex = 0;   // 現在の問題のインデックス
    public bool isCorrect = false;      // 回答が正解かどうか
    private Sound sound;                // 音声再生
    private int preAnswer = -1;         // 前回の回答
    private int preAnswerIndex = -1;    // 前回の回答の種類
    private int correctAnswerNum = 0;   // 正解数
    private bool anserAble = true;        // 回答可能かどうか
    private bool isFin = false;          // ゲームが終了したかどうか
    [SerializeField] private TMP_Text scentence;                // 問題文を表示するテキスト
    [SerializeField] private TMP_Text answerText;               // 回答を表示するテキスト
    [SerializeField] private TMP_Text correctAnswerNumText;     // 正解数を表示するテキスト
    [SerializeField] private TransitionSettings transition;
    [SerializeField] private float lodadDelay = 1.0f;
    [SerializeField] private string sceneName = "Result";
    [SerializeField] private GameObject correctSymbol;          // 正解のシンボル
    [SerializeField] private GameObject wrongSymbol;            // 不正解のシンボル

    void Start()
    {
        sound = GetComponent<Sound>();
        scentence.text = quests[currentQuestIndex].question;
        answerText.text = "";
        // GlobalVariables.questsCount = quests.Count;             // 問題数をグローバル変数に保存
        GlobalVariables.questsCount = 3;
        GlobalVariables.correctAnswerCount = 0;                 // 正解数をグローバル変数に保存
        correctSymbol.SetActive(false);
        wrongSymbol.SetActive(false);
    }

    void Update()
    {
        if (anserAble)
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
                wrongSymbol.SetActive(true);
                Invoke("HideWrongSymbol", 0.5f);
            }
        }
    }

    private void CorrectAnswer()
    {
        if (!isCorrect)
        {
            isCorrect = true;
            correctAnswerNum++;
            if(currentQuestIndex < GlobalVariables.questsCount - 5)
            {
                correctAnswerNumText.text = correctAnswerNum.ToString();
            }
            ShowAnswer();
            correctSymbol.SetActive(true);
            sound.PlayCorrectAnswer();
        }
    }

    public void ResetAnser()
    {
        answer = -1;
        answerIndex = -1;
        answerText.text = "";
        correctSymbol.SetActive(false);
        wrongSymbol.SetActive(false);
        anserAble = true;
        isCorrect = false;
    }

    public void ShowAnswer()
    {
        answerText.text = Regex.Unescape(quests[currentQuestIndex].answerText);
        Invoke("anserAbleFalse", 0.5f); // 0.5秒後に回答を無効にしてるけどいらないか...？
    }

    void anserAbleFalse(){
        anserAble = false;
    }

    public void NextQuest()
    {
        if (currentQuestIndex < GlobalVariables.questsCount - 1)
        {
            ResetAnser();
            currentQuestIndex++;
            scentence.text = quests[currentQuestIndex].question;
            if(currentQuestIndex >= GlobalVariables.questsCount - 5)
            {
                correctAnswerNumText.text = "?";
            }
        }
        else if (!isFin)
        {
            Fin();
        }
    }

    private void HideWrongSymbol()
    {
        wrongSymbol.SetActive(false);
    }

    private void Fin()
    {
        ResetAnser();
        GlobalVariables.correctAnswerCount = correctAnswerNum;
        anserAble = false;
        isFin = true;
        sound.StopAudio();
        sound.PlayFin();
        scentence.text = "タイムショック";
        scentence.fontSize = 100;
        scentence.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        TransitionManager.Instance().Transition(sceneName, transition, lodadDelay);
    }
}

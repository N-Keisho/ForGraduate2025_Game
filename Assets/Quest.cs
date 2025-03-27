using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum QuestType
{
    Open,
    Select,
    Silhouette
}

// 問題の情報を保持するクラス
[CreateAssetMenu(fileName = "Quest", menuName = "Quest", order = 0)]
public class Quest : ScriptableObject
{
    public QuestType type;
    public string question;
    public Sprite image;
    public List<string> answer;

    public bool CheckFormat()
    {
        if (type == QuestType.Open)
        {
            return answer.Count > 0;
        }
        else if (type == QuestType.Select)
        {
            return answer.Count > 1;
        }
        else if (type == QuestType.Silhouette)
        {
            return answer.Count > 0 && image != null;
        }
        Debug.LogError("正しく入力されていません: " + type);
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

// 音声認識を行うクラス
// アイテムの名前をキーワードとして登録し、認識されたキーワードに対応するアイテムのフラグをtrueにする
public class SpeechAnswer : MonoBehaviour
{
    private GameManager gm;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    void Start()
    {
        gm = GetComponent<GameManager>();
        foreach (Quest quest in gm.quests)
        {
            if (quest.CheckFormat()) // 問題のフォーマットが正しいかチェック
            {
                foreach (string answer in quest.answer)
                {
                    keywords.Add(answer, () =>
                    {
                        Debug.Log("Keyword: " + answer);
                        try
                        {
                            gm.answer = gm.quests.IndexOf(quest);
                            gm.answerIndex = quest.answer.IndexOf(answer);
                        }
                        catch (System.Exception e)
                        {
                            Debug.Log(e);
                        }
                    });
                }
            }
            else{
                Debug.LogError("問題のフォーマットが正しくありません: " + quest.name);
            }
        }

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
        else {
            gm.answer = -1;
            gm.answerIndex = -1;
        }
    }
}

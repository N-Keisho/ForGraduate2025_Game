using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

// 音声認識を行うクラス
// アイテムの名前をキーワードとして登録し、認識されたキーワードに対応するアイテムのフラグをtrueにする
public class SpeechAnswer : MonoBehaviour
{
    [SerializeField] private QuestList ql;
    [SerializeField] private GameManager gm;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    void Start()
    {
        foreach (Quest quest in ql.quests)
        {
            foreach (string answer in quest.answer)
            {
                keywords.Add(answer, () =>
                {
                    Debug.Log("Keyword: " + answer);
                    try
                    {
                        gm.answerIndex = ql.quests.IndexOf(quest);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                });
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
    }
}

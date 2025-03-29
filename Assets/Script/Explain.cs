using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyTransition;
public class Explain : MonoBehaviour
{
    [SerializeField] private int index = 0;
    [SerializeField] private List<GameObject> explainList = new List<GameObject>();
    [SerializeField] private TransitionSettings transition;
    [SerializeField] private float lodadDelay = 1.0f; // ロード遅延時間
    [SerializeField] private string sceneName = "LoadMain"; // ロードするシーン名
    private bool isLoading = false; // シーンがロード中かどうか
    void Start()
    {
        foreach(GameObject obj in explainList)
        {
            obj.SetActive(false); // 全てのオブジェクトを非表示にする
        }
        explainList[index].SetActive(true); // 最初のオブジェクトを表示する
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            NextExplain();
        }
    }

    public void NextExplain()
    {
        index++;
        if(index < explainList.Count)
        {
            explainList[index - 1].SetActive(false);
            if(index < explainList.Count)
            {
                explainList[index].SetActive(true); 
            }
        }
        else if(!isLoading)
        {
            isLoading = true;
            TransitionManager.Instance().Transition(sceneName, transition, lodadDelay);
        }
    }
}

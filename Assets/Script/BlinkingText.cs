using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlinkingText : MonoBehaviour
{
    private TMP_Text text;
    [SerializeField] private float blinkInterval = 1.5f; // Blink interval in seconds

    void Start(){
        text = GetComponent<TMP_Text>();
    }
    void Update()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.PingPong(Time.time / blinkInterval, 0.5f) + 0.5f);
    }
}

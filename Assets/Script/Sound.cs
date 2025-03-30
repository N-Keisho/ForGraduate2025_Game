using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioClip correctAnswer;
    [SerializeField] private AudioClip wrongAnswer;
    [SerializeField] private AudioClip denger;
    [SerializeField] private AudioClip start;
    [SerializeField] private AudioClip fin;
    private AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // PlayStart();
    }

    public void PlayCorrectAnswer()
    {
        audioSource.PlayOneShot(correctAnswer);
    }

    public void PlayWrongAnswer()
    {
        audioSource.PlayOneShot(wrongAnswer);
    }

    public void PlayDenger()
    {
        audioSource.PlayOneShot(denger);
        Invoke("Denger", 2f);
        Invoke("Denger", 4f);
    }
    void Denger(){
        audioSource.PlayOneShot(denger);
    }

    public void PlayStart()
    {
        audioSource.PlayOneShot(start);
    }

    public void PlayFin()
    {
        audioSource.PlayOneShot(fin);
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }
}

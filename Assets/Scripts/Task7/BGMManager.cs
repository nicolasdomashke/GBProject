using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{

    private AudioSource audioSource;
    [SerializeField] AudioClip gameClip;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void OnStartButtonClick()
    {
        audioSource.clip = gameClip;
        audioSource.Play();
    }
}
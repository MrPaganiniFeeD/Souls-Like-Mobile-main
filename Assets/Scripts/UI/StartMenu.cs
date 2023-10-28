using System;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    
    
    public event Action StartedGame;

    public void StartGame() => 
        StartedGame?.Invoke();

    private void Awake()
    {
        var audioSource = GetComponent<AudioSource>();
        audioSource.clip = _audioClip;
        audioSource.Play();
    }
}

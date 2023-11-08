using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSoundManager : MonoBehaviour
{
    public SoundManager soundManager;
    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }
    public void Play(AudioClip clip)
    {
        soundManager = FindObjectOfType<SoundManager>();
        soundManager.Play(clip);
    }
}

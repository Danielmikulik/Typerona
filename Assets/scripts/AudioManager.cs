using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

    [SerializeField] private Sound[] sounds;

    //singleton
    private static AudioManager instance;

    public static AudioManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        if (Instance == null)
        {           
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        //create sound sources for each sound
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.loop = sound.loop;
        }
    }

    private void Start()
    {
        Play("ThemeSound");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {           
            s.source.Play();
        }
    }

    public void ChangeMusicVolume(float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == "ThemeSound");
        s.source.volume = volume;
    }

    public void IncreaseMusicVolumeGradually(float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == "ThemeSound");
        StartCoroutine(IncreaseMusicVolumeGradually_Coroutine(s.source, volume));
    }

    public void ChangeSFXVolume(float volume)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name != "ThemeSound")
            {
                sound.source.volume = volume;
            }            
        }
    }

    private IEnumerator IncreaseMusicVolumeGradually_Coroutine(AudioSource source, float musicVolume)
    {
        while (source.volume < musicVolume)
        {
            source.volume += 0.002f;
            yield return null;
        }       
    }
}

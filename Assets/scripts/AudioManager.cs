using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

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
        //Debug.Log("pred " + s.source.volume);
        s.source.volume = volume;
        //Debug.Log("po " + s.source.volume);
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
}

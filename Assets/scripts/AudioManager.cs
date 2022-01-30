using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages all sounds in game.
/// </summary>
public class AudioManager : MonoBehaviour {

    [SerializeField] private Sound[] sounds;

    //singleton
    private static AudioManager instance;

    /// <summary>
    /// static instance of AudioManager. Static is needed for sound to be consistent between scenes.
    /// </summary>
    public static AudioManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        if (Instance == null)
        {           
            Instance = this;
        }
        else
        {
            Destroy(gameObject);    //Destroy this AudioManager object, because one is already present.
            return;
        }

        DontDestroyOnLoad(gameObject);  //Ensures that gameObject is not destroyed on scene change.

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

    /// <summary>
    /// Plays a sound specified by name.
    /// </summary>
    /// <param name="name">Name of the sound to be played</param>
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {           
            s.source.Play();
        }
    }

    /// <summary>
    /// Change of the music volume.
    /// </summary>
    /// <param name="volume">Volume value</param>
    public void ChangeMusicVolume(float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == "ThemeSound");
        s.source.volume = volume;
    }

    /// <summary>
    /// Gradually increases music volume.
    /// </summary>
    /// <param name="volume"></param>
    public void IncreaseMusicVolumeGradually(float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == "ThemeSound");
        StartCoroutine(IncreaseMusicVolumeGradually_Coroutine(s.source, volume));
    }


    /// <summary>
    /// Change of the SFX volume.
    /// </summary>
    /// <param name="volume">SFX value</param>
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

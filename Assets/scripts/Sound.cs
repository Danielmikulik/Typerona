using UnityEngine;

/// <summary>
/// Data class to store information about sound.
/// </summary>
[System.Serializable]
public class Sound {

    public string name;
    public bool loop;
    public AudioClip clip;

    [HideInInspector]
    public AudioSource source;
    
}

﻿using UnityEngine;

/// <summary>
/// Spawning of new virus particles on the scene.
/// </summary>
public class WordSpawner : MonoBehaviour
{
    [SerializeField] private GameObject virus;
    
    /// <summary>
    /// Spawns a new virus particle on the scene at random location.
    /// </summary>
    /// <returns>New virus particle with word</returns>
    public WordDisplay SpawnWord()
    {
        float x = Random.Range(-10f, 10f);
        float y = Random.Range(-5f, 6f);
        float z = 5f;
        GameObject virusObj = Instantiate(virus, new Vector3(x, y, z), Quaternion.identity);
        //display sits on grandchild of virus prefab

        WordDisplay wordDisplay = virusObj.transform.GetChild(1).transform.GetChild(0).GetComponent<WordDisplay>();

        return wordDisplay;
    }
}

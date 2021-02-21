using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public WordManager wordManager;

    [SerializeField]
    private float wordDelay = 1.5f;
    private float nextWordTime = 0f;
    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextWordTime)
        {
            wordManager.AddWord();
            nextWordTime = Time.time + wordDelay;
            if (wordDelay > 0.75f)
            {
                wordDelay *= .99f;
            }         
        }
    }
}

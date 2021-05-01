using UnityEngine;

public class Timer : MonoBehaviour
{
    public WordManager wordManager;

    [SerializeField]
    private float wordDelay = 2.5f;
    private float nextWordTime = 0.1f;
    // Update is called once per frame
    private void Update()
    {
        if (Time.time >= nextWordTime)
        {
            wordManager.AddWord();          
            nextWordTime = Time.time + wordDelay;
            if (wordDelay > 1.5f)
            {
                wordDelay *= .99f;
            }         
        }
    }
}

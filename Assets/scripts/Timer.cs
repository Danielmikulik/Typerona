using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private WordManager wordManager;

    [SerializeField] private float wordDelay = 2.5f;    //delay between virus spawns

    private int proCount = 30;
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
            else
            {
                if (proCount > 0)
                {
                    proCount--;
                }
                else
                {
                    if (wordDelay > 1.2f)
                    {
                        wordDelay *= .99f;
                    }
                }
            }        
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Disinfection : MonoBehaviour
{
    public ParticleSystem particles;

    public void DestroyAllViruses()
    {
        StartCoroutine(Disinfect());  
    }

    private IEnumerator<WaitForSeconds> Disinfect()
    {
        Debug.Log(Time.time);
        WordManager wordManager = GameObject.FindGameObjectWithTag("WordManager").GetComponent<WordManager>();
        Debug.Log(Time.time);
        Instantiate(particles);
        yield return new WaitForSeconds(particles.main.duration / 2);
        GameObject[] viruses = GameObject.FindGameObjectsWithTag("Virus");
        for (int i = 0; i < viruses.Length; i++)
        {
            GameObject virus = viruses[i];
            Destroy(virus);
        }
        wordManager.ClearList();
    }
}

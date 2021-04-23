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
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        WordManager wordManager = GameObject.FindGameObjectWithTag("WordManager").GetComponent<WordManager>();
        audioManager.Play("Spray");
        Instantiate(particles);        
        yield return new WaitForSeconds(particles.main.duration / 2);
        GameObject[] viruses = GameObject.FindGameObjectsWithTag("Virus");
        for (int i = 0; i < viruses.Length; i++)
        {
            GameObject virus = viruses[i];
            Destroy(virus);
        }
        wordManager.ClearWordList();
        yield return new WaitForSeconds(1.2f);
        audioManager.Play("DisinfectionEmpty");
    }
}

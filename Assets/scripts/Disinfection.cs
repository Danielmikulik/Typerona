using System.Collections.Generic;
using UnityEngine;

public class Disinfection : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;

    public ParticleSystem Particles { get => particles; set => particles = value; }

    public void DestroyAllViruses()
    {
        StartCoroutine(Disinfect());  
    }

    private IEnumerator<WaitForSeconds> Disinfect()
    {
        AudioManager audioManager = AudioManager.Instance;
        WordManager wordManager = GameObject.FindGameObjectWithTag("WordManager").GetComponent<WordManager>();
        audioManager.Play("Spray");
        Instantiate(Particles);        
        yield return new WaitForSeconds(Particles.main.duration / 2);

        GameObject[] viruses = GameObject.FindGameObjectsWithTag("Virus");
        for (int i = 0; i < viruses.Length; i++)
        {
            GameObject virus = viruses[i];
            Destroy(virus);
        }

        wordManager.ClearWordList();    //clears list of words in wordManager, so they can be used again
        yield return new WaitForSeconds(1.2f);
        audioManager.Play("DisinfectionEmpty");
    }
}

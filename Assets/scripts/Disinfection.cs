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
        Instantiate(particles);
        Debug.Log("som tuuuuuuuu");
        yield return new WaitForSeconds(2f);
        Debug.Log("som tuuuu");
        GameObject[] viruses = GameObject.FindGameObjectsWithTag("Virus");
        Debug.Log("som tu");
        foreach (GameObject virus in viruses)
        {
            Destroy(virus);
            Debug.Log("virus dead");
        }
        yield return new WaitForSeconds(2f);
    }
}

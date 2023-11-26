using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCollector : MonoBehaviour
{
    public GameObject[] Doors;
    public GameObject[] Platforms;
    public bool normalCat = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cat") && !other.gameObject.GetComponent<CatFollow>().enabled && normalCat)
        {
            foreach (GameObject d in Doors)
            {
                GrowCounter gc = d.GetComponent<GrowCounter>();
                gc.plantsToGrow--;
            }
            /*foreach (GameObject p in Platforms)
            {
                CatPlatform ps = p.GetComponent<CatPlatform>();
                ps.numCats++;
            }*/
            other.enabled = true;
            other.gameObject.GetComponent<CatFollow>().enabled = true;
        }
    }

    public void CollectCat()
    {
        foreach (GameObject d in Doors)
        {
            GrowCounter gc = d.GetComponent<GrowCounter>();
            gc.plantsToGrow--;
        }
    }
}

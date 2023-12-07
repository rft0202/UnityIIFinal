using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCollector : MonoBehaviour
{
    GameManager gameManager;
    public GameObject[] Doors;
    public GameObject[] Platforms;
    public bool normalCat = true;

    public void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cat") && !other.gameObject.GetComponent<CatFollow>().enabled && normalCat)
        {
            CollectCat(other.gameObject);
            /*foreach (GameObject p in Platforms)
            {
                CatPlatform ps = p.GetComponent<CatPlatform>();
                ps.numCats++;
            }*/
            other.enabled = true;
            other.gameObject.GetComponent<CatFollow>().enabled = true;
        }
    }

    public void CollectCat(GameObject cat)
    {
        foreach (GameObject d in Doors)
        {
            GrowCounter gc = d.GetComponent<GrowCounter>();
            gc.plantsToGrow--;
        }
        DontDestroyOnLoad(cat);
    }
}

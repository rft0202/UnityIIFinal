using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticles : MonoBehaviour
{
    public GameObject wateringCan;
    public GameObject waterParticles;
    PickupObj pickupObj;

    // Start is called before the first frame update
    void Start()
    {
        pickupObj = wateringCan.GetComponent<PickupObj>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pickupObj.pickedUp)
        {
            waterParticles.SetActive(true);
        }
        else
        {
            waterParticles.SetActive(false);
        }
    }
}

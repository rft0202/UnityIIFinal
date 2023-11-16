using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowScript : MonoBehaviour
{
    public GameObject plant;
    public GrowCounter growCounter1;
    public GrowCounter growCounter2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WaterParticle"))
        {
            Grow();
        }
    }

    void Grow()
    {
        plant.transform.localScale = new Vector3(2, 2, 2);
        growCounter1.plantsToGrow--;
        growCounter2.plantsToGrow--;
    }
}

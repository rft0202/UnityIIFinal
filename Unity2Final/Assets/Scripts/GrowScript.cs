using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowScript : MonoBehaviour
{
    public GrowCounter growCounter1;
    public GrowCounter growCounter2;

    public AudioClip growSound;
    AudioSource sfx;

    private void Start()
    {
        sfx = GetComponent<AudioSource>();
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
        transform.localScale = new Vector3(2, 2, 2);
        growCounter1.plantsToGrow--;
        growCounter2.plantsToGrow--;
        GetComponent<Collider>().enabled = false;
        sfx.PlayOneShot(growSound);
    }
}

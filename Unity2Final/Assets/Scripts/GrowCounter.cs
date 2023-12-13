using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowCounter : MonoBehaviour
{
    public int plantsToGrow;
    public Animator door;

    AudioSource sfx;
    public AudioClip doorSqueak;
    bool doorOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        sfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(plantsToGrow <= 0 && !doorOpened)
        {
            door.SetTrigger("OpenDoor");
            doorOpened = true;
            sfx.PlayOneShot(doorSqueak);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowCounter : MonoBehaviour
{
    public int plantsToGrow;
    public Animator door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(plantsToGrow <= 0)
        {
            door.SetTrigger("OpenDoor");
        }
    }
}

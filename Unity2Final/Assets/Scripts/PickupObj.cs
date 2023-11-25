using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObj : MonoBehaviour
{
    public bool pickedUp=false;
    Rigidbody rb;
    Transform DestinationObj;
    public Renderer glowRender;
    public float glowSize = 1.05f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        DestinationObj = GameObject.Find("objTransform").transform;
        if(glowRender==null)
            glowRender = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pickedUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, DestinationObj.position, Vector3.Magnitude(transform.position - DestinationObj.position) / 4);
            transform.rotation = Quaternion.Lerp(transform.rotation, DestinationObj.rotation, 0.2f);
        }
    }

    public void showGlow()
    {
        glowRender.materials[1].SetFloat("_Scale", glowSize);
    }

    public void pickUp()
    {
        pickedUp = !pickedUp;

        if (rb != null)
        {
            if (pickedUp)
            {
                rb.useGravity = false;
                rb.isKinematic = true;
                //transform.position = DestinationObj.position;
                //transform.parent = DesinationObj;
            }
            else
            {
                rb.useGravity = true;
                rb.isKinematic = false;
                rb.velocity = (DestinationObj.position - transform.position) * 1500 * Time.deltaTime;
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, 30);
                //transform.parent = null;
            }
        }

    }
}

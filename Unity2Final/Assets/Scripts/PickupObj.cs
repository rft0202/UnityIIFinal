using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class PickupObj : MonoBehaviour
{
    public bool pickedUp=false;
    Rigidbody rb;
    Transform DestinationObj;
    public Renderer glowRender;
    public float glowSize = 1.05f;
    Vector3 startPos;
    Quaternion startRotation;
    [NonSerialized]
    public bool canDamage=false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        DestinationObj = GameObject.Find("objTransform").transform;
        startPos = transform.position;
        startRotation = transform.rotation;
        if(glowRender==null)
            glowRender = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pickedUp)
        {
            transform.SetPositionAndRotation(Vector3.MoveTowards(transform.position, DestinationObj.position, Vector3.Magnitude(transform.position - DestinationObj.position) / 4), Quaternion.Lerp(transform.rotation, DestinationObj.rotation, 0.2f));
        }
        else
        {
            if(transform.position.y<-5) resetPos();
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

    public void throwObj()
    {
        pickedUp = false;
        if(rb != null)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.velocity = transform.forward * 15;
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 30);
            StartCoroutine(damageCooldown());
        }
    }

    public void resetPos()
    {
        transform.SetPositionAndRotation(startPos, startRotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!pickedUp && collision.gameObject.CompareTag("OutOfBounds")) resetPos();
    }

    IEnumerator damageCooldown()
    {
        canDamage = true;
        yield return new WaitForSeconds(0.5f);
        canDamage = false;
    }
}

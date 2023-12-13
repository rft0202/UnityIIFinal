using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class CatPlatform : MonoBehaviour
{
    public Transform startPos, endPos;
    public float spd = 1.0f;
    public int numCats=0, requiredCats=0;
    public bool resets=false;
    float distMoved=4.71f;

    Vector3 _startPos, _endPos;
    [NonSerialized]
    public bool playerOn=false;

    public NavMeshLink enterLink, exitLink;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = (startPos == null) ? (transform.position) : (startPos.position);
        _endPos = endPos.position;
        spd *= 0.02f;
        requiredCats *= 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (numCats >= requiredCats && playerOn)
        {
            distMoved+=spd;
            if (distMoved >= 6.283) distMoved -= 6.283f; //Why these weird decimals??? Well it seems the Sin function
            transform.position = Vector3.Lerp(_startPos, _endPos, (Mathf.Sin(distMoved)+1)/2); //uses radians :(
        }else if (resets)
        {
            if (distMoved > 4.71f + spd || distMoved < 4.71f - spd) //4.71 = 3/2 pi
            {
                distMoved += spd;
                if (distMoved >= 6.283) distMoved -= 6.283f; // 6.283 = 2pi
                transform.position = Vector3.Lerp(_startPos, _endPos, (Mathf.Sin(distMoved) + 1) / 2);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
            playerOn = true;
        }else if (other.gameObject.CompareTag("Cat"))
        {
            //other.transform.SetParent(transform);
            other.gameObject.GetComponent<CatFollow>().onPlatform(gameObject);
            numCats++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
            playerOn = false;
        }else if (other.gameObject.CompareTag("Cat"))
        {
            //other.transform.SetParent(null);
            other.gameObject.GetComponent<CatFollow>().offPlatform();
            numCats--;
        }
    }
}

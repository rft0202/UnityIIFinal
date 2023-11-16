using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPlatform : MonoBehaviour
{
    public Transform startPos, endPos;
    public float spd = 1.0f;
    public int numCats=0, requiredCats=0;
    float distMoved=4.6f;
    Vector3 _startPos, _endPos;
    bool playerOn=false;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = (startPos == null) ? (transform.position) : (startPos.position);
        _endPos = endPos.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (numCats >= requiredCats && playerOn)
        {
            distMoved+=spd*Time.deltaTime;
            if (distMoved >= 360) distMoved -= 360;
            transform.position = Vector3.Lerp(_startPos, _endPos, (Mathf.Sin(distMoved)+1)/2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
            playerOn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
            playerOn = false;
        }
    }
}

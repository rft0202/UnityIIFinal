using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class MovingNavLink : MonoBehaviour
{
    public Transform endPos;
    NavMeshLink link;
    public float maxLength;
    // Start is called before the first frame update
    void Start()
    {
        link = GetComponent<NavMeshLink>();
        link.startPoint = transform.position+link.startPoint;
        link.endPoint = transform.position+link.endPoint;
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(link.startPoint, endPos.position);
        link.endPoint = (dist>maxLength)?(Vector3.up*100):(endPos.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFollow : MonoBehaviour
{
    public float spd;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = new Vector3(player.position.x, player.position.y - 0.8f, player.position.z);
        transform.LookAt(playerPos);
        transform.position = Vector3.MoveTowards(transform.position, playerPos, (Vector3.Distance(transform.position,playerPos))*spd*Time.deltaTime);
    }
}

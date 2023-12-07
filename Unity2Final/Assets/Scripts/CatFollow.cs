using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CatFollow : MonoBehaviour
{
    //public float spd;
    Transform player;
    NavMeshAgent agent;
    GameObject plat;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        StartF();
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 playerPos = new Vector3(player.position.x, player.position.y - 0.8f, player.position.z);
        transform.LookAt(playerPos);
        transform.position = Vector3.MoveTowards(transform.position, playerPos, (Vector3.Distance(transform.position,playerPos))*spd*Time.deltaTime);*/
        if (gm.sceneChange)
        {
            StartF();
            transform.position = player.position;
        }
        agent.SetDestination(player.position);
        if (plat != null)
        {
            transform.position = new Vector3(transform.position.x, plat.transform.position.y, transform.position.z);
            if (!plat.GetComponent<CatPlatform>().playerOn)
            {
                bool playerPassedPlat = (Vector3.Distance(player.position,plat.transform.GetChild(2).position) < Vector3.Distance(player.position,plat.transform.GetChild(1).position));

                Vector3 targetPos;
                CatPlatform platScript = plat.GetComponent<CatPlatform>();
                if (playerPassedPlat)
                {
                    targetPos = platScript.exitLink.startPoint;
                }
                else
                {
                    targetPos = platScript.enterLink.startPoint;
                }

                agent.SetDestination(targetPos);
            }
            else
            {
                agent.SetDestination(transform.position);
            }
        }
    }

    public void onPlatform(GameObject _p)
    {
        plat = _p;
    }

    public void offPlatform()
    {
        plat = null;
    }

    void StartF()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}

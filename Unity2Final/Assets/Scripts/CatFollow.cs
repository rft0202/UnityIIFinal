using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CatFollow : MonoBehaviour
{
    public bool platformCat=false;
    Transform player;
    NavMeshAgent agent;
    GameObject plat;
    string currScene="";

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        StartF();
        currScene = SceneManager.GetActiveScene().name;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 playerPos = new Vector3(player.position.x, player.position.y - 0.8f, player.position.z);
        transform.LookAt(playerPos);
        transform.position = Vector3.MoveTowards(transform.position, playerPos, (Vector3.Distance(transform.position,playerPos))*spd*Time.deltaTime);*/
        if (currScene != SceneManager.GetActiveScene().name)
        {
            currScene = SceneManager.GetActiveScene().name;
            StartF();
            CatReset();
        }
        if (player == null)
        {
            StartF();
            if(gm.CatCollected(gameObject))
                CatReset();
        }
        agent.SetDestination(player.position);
        if (plat != null)
        {
            //agent.enabled = false;
            transform.position = new Vector3(transform.position.x, plat.transform.position.y, transform.position.z);
            //agent.enabled = true;
            /*if (!plat.GetComponent<CatPlatform>().playerOn)
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

                agent.SetDestination(player.position);
            }
            else
            {
                agent.SetDestination(transform.position);
            }*/
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

    public void StartF()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    public void CatReset()
    {
        agent.enabled = false;
        transform.position = player.position;
        agent.enabled = true;
    }
}

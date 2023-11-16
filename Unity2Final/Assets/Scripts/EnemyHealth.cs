using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public Image hpBar;
    public float maxHp = 100, currHp;
    public PlayerHealth ph;
    public GameObject avatar, ragdoll;

    Coroutine unalert;

    // Start is called before the first frame update
    void Start()
    {
        currHp = maxHp;
        ph = GetComponent<PatrolEnemy>().target.gameObject.GetComponent<PlayerHealth>();
    }

    public void TakeDamage(float _dmg)
    {
        currHp -= _dmg;
        hpBar.fillAmount = currHp / maxHp;
        if (currHp <= 0)
        {
            //hpBar.transform.parent.gameObject.SetActive(false);
            //Enemy died
            Dead();
        }

        if(unalert!=null) StopCoroutine(unalert);
        GetComponent<PatrolEnemy>().alerted = true;
        unalert = StartCoroutine(GetComponent<PatrolEnemy>().Unalert());
    }

    void Dead()
    {
        avatar.SetActive(false);
        //ragdoll.SetActive(true);
        GetComponent<PatrolEnemy>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        ph.enemyKilled();
        //Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Image hpBar;
    public float maxHp = 100;
    float currHp;
    public int enemiesInLevel;
    int enemiesKilled;
    // Start is called before the first frame update
    void Start()
    {
        currHp = maxHp;
    }

    public void TakeDamage(float _dmg)
    {
        currHp -= _dmg;
        hpBar.fillAmount = currHp / maxHp;
        if (currHp <= 0)
        {
            hpBar.fillAmount = 0;
            //Player died
            playerDie();

            //player die anim (cam falling)
            //Have event at the end of anim that restarts level

            //stop player control
            GetComponent<FPMovement>().enabled = false;
            GetComponent<MouseLook>().enabled = false;
            transform.GetChild(0).GetComponent<MouseLook>().enabled = false;
        }
    }

    public void enemyKilled()
    {
        enemiesKilled++;
        if (enemiesKilled >= enemiesInLevel)
        {
            StartCoroutine(nextLvl(SceneManager.GetActiveScene().name));
        }
    }

    IEnumerator nextLvl(string _lvl)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(_lvl);
    }

    public void playerDie()
    {
        StartCoroutine(nextLvl(SceneManager.GetActiveScene().name));
    }
}

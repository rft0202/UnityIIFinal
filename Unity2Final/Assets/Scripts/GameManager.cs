using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public int catsFollowing;
    string currScene="";
    public bool sceneChange=false;
    public GameObject[] cats = new GameObject[30];
    GameObject[] dupeCheck;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        sceneChange = false;
        if (currScene != SceneManager.GetActiveScene().name)
        {
            sceneChange = true;
            currScene = SceneManager.GetActiveScene().name;
        }
    }

    public void AddCat(GameObject cat)
    {
        cats[catsFollowing] = cat;
        catsFollowing++;
    }
    public void PlayerDie()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        for(int i=0; i<catsFollowing; i++)
        {
            CatFollow _cat = cats[i].GetComponent<CatFollow>();
            _cat.StartF();
            _cat.CatReset();
        }
        StartCoroutine(CheckDupeCats());
    }

    public bool CatCollected(GameObject cat)
    {
        for(int i=0; i<catsFollowing; i++)
        {
            if (cats[i] == cat) return true;
        }
        return false;
    }

    IEnumerator CheckDupeCats()
    {
        yield return new WaitForSeconds(1);
        //Get list of all Cats
        dupeCheck = GameObject.FindGameObjectsWithTag("Cat");

        //Check all cats againsts each other
        for(int i =0; i<dupeCheck.Length; i++)
        {
            for(int j=i+1; j<dupeCheck.Length; j++)
            {
                if (i!=j && dupeCheck[i].name == dupeCheck[j].name) //If two cats have the same name (oh no, dupe!)
                {
                    //Prioritize destroying the cat that hasn't been collected (if reversed player loses cat when die)
                    Destroy((!dupeCheck[i].GetComponent<CatFollow>().enabled) ? (dupeCheck[i]) : (dupeCheck[j]));
                }
            }
        }
        
    }
}

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
    public GameObject[] cats = new GameObject[20];
    GameObject[] dupeCheck;

    public CatsInScene CatsInScene;
    public int catsCollectedGM;

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
        CatsInScene = GameObject.Find("Cat Count").GetComponent<CatsInScene>();
    }

    // Update is called once per frame
    void Update()
    {
        sceneChange = false;
        if (currScene != SceneManager.GetActiveScene().name)
        {
            CatsInScene = GameObject.Find("Cat Count").GetComponent<CatsInScene>();
            sceneChange = true;
            currScene = SceneManager.GetActiveScene().name;
            StartCoroutine(CheckDupeCats());
        }
    }

    public void AddCat(GameObject cat)
    {
        cats[getNextIndex()] = cat;
        catsFollowing = getNextIndex();
    }
    public void PlayerDie()
    {
        catsCollectedGM = CatsInScene.catsCollected; //save number of cats collected
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

    int getNextIndex()
    {
        for(int i=0; i<20; i++)
        {
            if (cats[i] == null) return i;
        }
        return -1;
    }

    IEnumerator CheckDupeCats()
    {
        yield return new WaitForSeconds(0.25f);
        //Get list of all Cats
        dupeCheck = GameObject.FindGameObjectsWithTag("Cat");

        //Check all cats againsts each other for duplicates
        for(int i =0; i<dupeCheck.Length; i++)
        {
            if (dupeCheck[i].GetComponent<CatFollow>() != null)
            {
                //If cat not collected & cat from different scene (does the cat belong here?)
                if (!dupeCheck[i].GetComponent<CatFollow>().enabled && dupeCheck[i].GetComponent<CatAnim>().fromScene != SceneManager.GetActiveScene().name)
                {
                    Destroy(dupeCheck[i]);
                }
                else //Else check for duplicates
                {
                    for (int j = i + 1; j < dupeCheck.Length; j++)
                    {
                        if (i != j && dupeCheck[i].name == dupeCheck[j].name) //If two cats have the same name (oh no, dupe!)
                        {
                            CatFollow cf = dupeCheck[i].GetComponent<CatFollow>();
                            //Player loses platform cats, but keeps regular cats
                            bool checkBool = (cf.platformCat) ? (cf.enabled) : (!cf.enabled);

                            //Prioritize destroying the cat that hasn't been collected (reversed if platform cat)
                            Destroy((checkBool) ? (dupeCheck[i]) : (dupeCheck[j]));

                            //if (checkBool) catsFollowing--;
                        }
                    }
                }
            }
        }
        
    }
}

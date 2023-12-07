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
    GameObject[] cats = new GameObject[30]; 

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else if(Instance != this)
        {
            Destroy(this);
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

    public void AddCat(GameObject cat) //uh dont think this is used (yet)
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public int catsFollowing;
    List<GameObject> cats;
    string currScene="";
    public bool sceneChange=false;

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
            for(int i=0; i<cats.Count; i++)
            {
                cats[i].transform.position = GameObject.Find("Player").transform.position;
            }
        }
    }

    public void AddCat(GameObject cat)
    {
        cats.Add(cat);
        DontDestroyOnLoad(cat);
        catsFollowing++;
    }
}

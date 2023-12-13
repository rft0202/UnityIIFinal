using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CatsInScene : MonoBehaviour
{
    string currScene = "";
    public int catsInScene = 0;
    public int catsCollected;
    public TMP_Text counter;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        currScene = SceneManager.GetActiveScene().name;
        catsCollected = gameManager.catsCollectedGM;
        switch (currScene)
        {
            case "Tutorial":
                catsInScene = 1;
                break;
            case "Puzzle":
                catsInScene = 2;
                break;
            case "AdversaryRoom":
                catsInScene = 3;
                break;
            case "PlatformingRoom":
                catsCollected = 0;
                catsInScene = 4;
                break;
            case "EscapeRoom":
                catsInScene = 5;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        counter.text = "Cats to Collect: " + catsCollected + "/" + catsInScene;
    }
}

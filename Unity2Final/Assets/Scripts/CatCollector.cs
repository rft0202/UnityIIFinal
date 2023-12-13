using UnityEngine;

public class CatCollector : MonoBehaviour
{
    GameManager gameManager;
    public GameObject[] Doors;
    public GameObject[] Platforms;
    public bool normalCat = true;
    public AudioClip[] catMeows;
    AudioSource sfx;

    public CatsInScene CatsInScene;

    public void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        sfx = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cat") && !other.gameObject.GetComponent<CatFollow>().enabled && normalCat)
        {
            CollectCat(other.gameObject);
            /*foreach (GameObject p in Platforms)
            {
                CatPlatform ps = p.GetComponent<CatPlatform>();
                ps.numCats++;
            }*/
            other.enabled = true;
            other.gameObject.GetComponent<CatFollow>().enabled = true;
        }
    }

    public void CollectCat(GameObject cat)
    {
        foreach (GameObject d in Doors)
        {
            GrowCounter gc = d.GetComponent<GrowCounter>();
            gc.plantsToGrow--;
        }
        //Debug.Log(cat);
        gameManager.AddCat(cat);
        cat.GetComponent<Collider>().enabled = false;
        sfx.PlayOneShot(catMeows[Random.Range(0, catMeows.Length)]);

        CatsInScene.catsCollected++;
    }
}

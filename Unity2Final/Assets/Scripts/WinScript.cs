using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(MovePlayer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MovePlayer()
    {
        yield return new WaitForSeconds(0.05f);
        transform.position = new Vector3(0, 0, 47);
    }
}

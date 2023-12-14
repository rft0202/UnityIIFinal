using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

[RequireComponent(typeof(CharacterController))]
public class FPMovement : MonoBehaviour
{
    public float spd=10, grav=-9.8f, gravMult=3, jumpPow=-10, coyoteTime=1,climbSpd=0.25f;
    float h, v;
    float vy;
    float coyoteTimer = 0;
    bool isCollector = true,onLadder=false, onRope=false;
    Collider prevRope;
    CharacterController controller;
    GameManager gm;

    AudioSource sfx;
    public AudioClip catCrash;

    //ItemCollector collector;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        try
        {
            //collector = GameObject.Find("ItemHUD").GetComponent<ItemCollector>();
            Renderer uhhhhh = GameObject.Find("uhhhhh").GetComponent<Renderer>();
        }
        catch (Exception e)
        {
            isCollector = false;
            if (e.Message == "") Debug.Log("");
        }
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        sfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5) gm.PlayerDie(); //Reset if player fall oob

        float moveX=h*spd, moveZ=v*spd;
        Vector3 movement = new Vector3(moveX, 0, moveZ);

        movement = Vector3.ClampMagnitude(movement,spd);

        coyoteTimer += Time.deltaTime;
        if (onRope)
        {
            movement = Vector3.zero;
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
        else if (onLadder && (v != 0 || h != 0))
        {
            vy = -1;
            movement.y = climbSpd;
        }
        else
        {
            if (isGrounded() && vy < 0)
            {
                coyoteTimer = 0;
                vy = -1;
            }
            else
            {
                vy += grav * gravMult * Time.deltaTime;
            }

            movement.y = vy;
        }
        movement*=Time.deltaTime;
        movement = transform.TransformDirection(movement);

        controller.Move(movement);
    }

    public void MoveInput(InputAction.CallbackContext ctx)
    {
        h = ctx.ReadValue<Vector2>().x;
        v = ctx.ReadValue<Vector2>().y;
        if (isCollector)
        {
            //if ((h != 0 || v != 0) && collector.itemTxt.text == "Use WASD to move")
                //collector.DoNextTutorial("Use Space to Jump");
        }
            
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            
            if (isGrounded() || (coyoteTimer<=coyoteTime) || onRope) {
                coyoteTimer = coyoteTime+1;
                vy = -1;
                vy *= jumpPow;
                if (isCollector)
                {
                    //if (collector.itemTxt.text == "Use Space to Jump")
                        //collector.DoNextTutorial("Collect all of the " + collector.itemName + "!");
                }
            }
            if (onRope)
            {
                onRope = false;
                transform.SetParent(null);
                StartCoroutine(RopeCooldown());
            }
        }
        else if(!ctx.performed&&vy>0)
        {
            vy *= .5f;
        }
    }

    bool isGrounded()
    {
        return controller.isGrounded;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            StartCoroutine(ChangeScene(other.gameObject.name, 1));
        }else if (other.CompareTag("Ladder"))
        {
            onLadder = true;
        }else if (other.CompareTag("Rope"))
        {
            onRope = true;
            prevRope = other;
            controller.enabled = false;
            transform.position = other.transform.GetChild(0).position;
            controller.enabled = true;
            transform.SetParent(other.transform.GetChild(0));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            onLadder = false;
        }
    }

    IEnumerator ChangeScene(string _scene,float _time)
    {
        if(SceneManager.GetActiveScene().name == "PlatformingRoom")
        {
            sfx.PlayOneShot(catCrash);
            yield return new WaitForSeconds(2.0f);
        }
        yield return new WaitForSeconds(_time);
        SceneManager.LoadScene(_scene);
    }

    IEnumerator RopeCooldown()
    {
        prevRope.enabled = false;
        yield return new WaitForSeconds(1f);
        prevRope.enabled = true;
    }
}

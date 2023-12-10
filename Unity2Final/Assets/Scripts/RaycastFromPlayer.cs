using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.AI.Navigation;

public class RaycastFromPlayer : MonoBehaviour
{
    public float dist = 5;
    bool canPickUp = true, holdingObj = false;
    public float pickupCooldown = 1;

    Collider heldObj;

    public GameObject clickIcon;

    GameObject prevHit;

    AudioSource sfxSrc;

    // Start is called before the first frame update
    void Start()
    {
        sfxSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward*dist, Color.green);

        try{prevHit.GetComponent<PickupObj>().glowRender.materials[1].SetFloat("_Scale", 0.01f);}
        catch(System.Exception e) { if (e.Message == "") Debug.Log(e); }
        clickIcon.SetActive(showClickIcon());
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PickupObj(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Cursor.lockState = CursorLockMode.Locked;
            interactObj();

            RaycastHit hit;

            if (holdingObj&&canPickUp)
            {
                heldObj.GetComponent<PickupObj>().pickUp();
                StartCoroutine(doPickupCooldown());
                heldObj = null;
                holdingObj = false;
            }
            else if (canPickUp&&Physics.Raycast(transform.position, transform.forward, out hit, dist))
            {
                if (isPickupObj(hit.collider))
                {
                    heldObj = hit.collider;
                    heldObj.GetComponent<PickupObj>().pickUp();
                    StartCoroutine(doPickupCooldown());
                    holdingObj = true;
                    
                }
            }
        }

    }

    public void ThrowObj(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && holdingObj)
        {
            Cursor.lockState = CursorLockMode.Locked;
            heldObj.GetComponent<PickupObj>().throwObj();
            StartCoroutine(doPickupCooldown());
            holdingObj = false;
            heldObj = null;
        }
    }

    IEnumerator doPickupCooldown()
    {
        canPickUp = false;
        yield return new WaitForSeconds(pickupCooldown);
        canPickUp = true;
    }

    public void interactObj()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, dist))
        {
            //if(hit.collider.compareTag("btnOrSomething")) doBtnThing;
        }
    }

    bool showClickIcon() //AND show outline highlight
    {
        bool returnBool = false;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, dist))
        {
            returnBool = (isPickupObj(hit.collider) && !holdingObj);

            if (returnBool)
            {
                prevHit = hit.collider.gameObject;
                prevHit.GetComponent<PickupObj>().showGlow();
            }
        }
        return returnBool;
    }

    bool isPickupObj(Collider hitCol)
    {
        return hitCol.gameObject.GetComponent<PickupObj>() != null;
    }
}

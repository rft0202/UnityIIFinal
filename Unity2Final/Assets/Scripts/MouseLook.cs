using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxis{
        MouseXY=0, MouseX=1, MouseY=2
    }

    public RotationAxis axis = RotationAxis.MouseXY;
    public float sensitivityX=10f, sentitivityY=10f;
    public float minVertical=-60f, maxVertical=60f;

    bool isCollector=true;

    float mouseX, mouseY, verticalRotation=0;

    //ItemCollector collector;

    // Start is called before the first frame update
    void Start()
    {
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb!=null) rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (axis)
        {
            case RotationAxis.MouseXY: //Both XY
                verticalRotation -= mouseY * sentitivityY;
                verticalRotation = Mathf.Clamp(verticalRotation, minVertical, maxVertical);

                float deltaRotation = mouseX * sensitivityX;
                float hozRotation = transform.localEulerAngles.y + deltaRotation;

                transform.localEulerAngles = new Vector3(verticalRotation, hozRotation, 0);
            break;
            case RotationAxis.MouseX: //Only X
                transform.Rotate(0,mouseX*sensitivityX,0);
            break;
            case RotationAxis.MouseY: //Only Y
                verticalRotation -= mouseY * sentitivityY;
                verticalRotation = Mathf.Clamp(verticalRotation, minVertical, maxVertical);

                transform.localEulerAngles = new Vector3(verticalRotation, transform.localEulerAngles.y, 0);
            break;
        }
    }

    public void LookValues(InputAction.CallbackContext ctx)
    {
        //Debug.Log(ctx.ReadValue<Vector2>());
        mouseX = ctx.ReadValue<Vector2>().x;
        mouseY = ctx.ReadValue<Vector2>().y;
        if (isCollector)
        {
            //if (collector.itemTxt.text == "Use the mouse to look around")
                //collector.DoNextTutorial("Use WASD to move");
        }
    }
}

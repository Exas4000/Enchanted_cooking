using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static bool pressSpace = false;
    public static bool pressE = false;
    public static bool pressQ = false;
    public static bool pressUp = false;
    public static bool pressDown = false;
    public static bool pressLeft = false;
    public static bool pressRight = false;
    public static bool confim = false;
    public static bool moving = false;
    public static int direction = 0;


    // Update is called once per frame
    void Update()
    {
        pressSpace = Input.GetKeyDown(KeyCode.Space);
        pressE = Input.GetKeyDown(KeyCode.E);
        pressQ = Input.GetKeyDown(KeyCode.Q);

        if (pressE || pressSpace)
        {
            confim = true;
        }
        else
        {
            confim = false;
        }


        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            pressUp = true;
            direction = 2;
        }
        else
        {
            pressUp = false;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            pressDown = true;
            direction = 0;
        }
        else
        {
            pressDown = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            pressLeft = true;
            direction = 1;
        }
        else
        {
            pressLeft = false;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            pressRight = true;
            direction = 3;
        }
        else
        {
            pressRight = false;
        }

        moving = (pressDown || pressLeft || pressRight || pressUp);

        /*
        if (pressDown || pressLeft || pressRight || pressUp)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        */
    }
}

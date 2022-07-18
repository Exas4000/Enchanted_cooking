using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myRB;
    private Vector2 movementVector = new Vector2(0, 0);
    [SerializeField] float speed = 100;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        if (InputManager.pressDown)
        {
            movementVector += new Vector2(0, -speed);
        }

        if (InputManager.pressUp)
        {
            movementVector += new Vector2(0, speed);
        }

        if (InputManager.pressRight)
        {
            movementVector += new Vector2(speed, 0);
        }

        if (InputManager.pressLeft)
        {
            movementVector += new Vector2(-speed, 0);
        }


        myRB.velocity = movementVector * Time.deltaTime;

        movementVector = new Vector2(0, 0);
    }

    public void ceaseMovement()
    {
        myRB.velocity = new Vector2(0, 0);
    }
}

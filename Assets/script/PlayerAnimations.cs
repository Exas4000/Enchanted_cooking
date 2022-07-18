using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator myself;

    void Start()
    {
        myself = GetComponent<Animator>();
    }

    
    void Update()
    {
        myself.SetBool("isMoving", InputManager.moving);
        myself.SetInteger("Direction", InputManager.direction);

    }
}

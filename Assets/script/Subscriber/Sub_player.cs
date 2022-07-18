using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sub_player : Subscriber
{

    PlayerMovement myMove;

    private void Awake()
    {
        myMove = GetComponent<PlayerMovement>();
    }

    public override void OnMenuActive()
    {
        myMove.ceaseMovement();
        myMove.enabled = false;
        //Debug.Log("movement disabled");
    }

    public override void OnResumeFromMenuActive()
    {    
        myMove.enabled = true;
        //Debug.Log("movement enabled");

    }

    public override void OnCutscene()
    {
        myMove.ceaseMovement();
        myMove.enabled = false;
    }

    public override void OnResumeFromCutscene()
    {
        myMove.enabled = true;
    }
}

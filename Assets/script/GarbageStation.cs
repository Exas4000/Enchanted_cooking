using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageStation : MonoBehaviour
{
    [SerializeField] Observer myObs;
    [SerializeField] Inventory playerInventory;

    [SerializeField] GameObject garbage; //garbage UI
    [SerializeField] GameObject objectVisual;

    private bool inRange = false;
    private int selectedPlayerItem = 0;
    private float inputBuffer = 0;

    private SoundFunctions mySounds;
    [SerializeField] AudioClip confirm;

    enum state
    {
        idle,
        menuMode
    }

    private state myState = state.idle;

    private void Start()
    {
        mySounds = GetComponent<SoundFunctions>();
    }

    void Update()
    {
        if (inputBuffer > 0)
        {
            inputBuffer -= Time.deltaTime;
        }

        switch (myState)
        {
            case state.idle:
                {
                    if (playerInventory.GetNumItem() > 0 && InputManager.confim && inRange)
                    {
                        myState = state.menuMode;
                        myObs.CallMenu(true);
                        garbage.SetActive(true);
                        updateVisual();
                        mySounds.playSound(confirm);
                        //enable visuals                        
                    }
                    return;
                }
            case state.menuMode:
                {
                    if (InputManager.pressQ)
                    {
                        myState = state.idle;
                        myObs.CallMenu(false);
                        selectedPlayerItem = 0;
                        garbage.SetActive(false);
                        //disable visuals
                    }

                    if (InputManager.confim)
                    {
                        playerInventory.RemoveSingleItem(selectedPlayerItem);
                        selectedPlayerItem = 0;
                        myState = state.idle;
                        myObs.CallMenu(false);
                        garbage.SetActive(false);
                        mySounds.playSound(confirm);

                        //disable visual
                    }

                    //changing the selection going toward first item
                    if (InputManager.pressLeft && inputBuffer <= 0)
                    {

                        selectedPlayerItem = DeterminePosition(selectedPlayerItem, true);
                        updateVisual();

                        inputBuffer = 0.2f;
                    }

                    //changing the selection going toward the last item
                    if (InputManager.pressRight && inputBuffer <= 0)
                    {

                        selectedPlayerItem = DeterminePosition(selectedPlayerItem, false);
                        updateVisual();

                        inputBuffer = 0.2f;

                    }

                    return;
                }
        }
    }

    private void updateVisual()
    {
        objectVisual.GetComponent<SpriteRenderer>().sprite = playerInventory.GetItemSprite(selectedPlayerItem);
    }

    private int DeterminePosition(int input, bool goToFirst)
    {
        int newNum = input;

        if (goToFirst)
        {
            if (newNum == 0)
            {
                newNum = playerInventory.GetNumItem() - 1;
            }
            else
            {
                newNum -= 1;
            }
        }
        else
        {
            if (newNum == playerInventory.GetNumItem() - 1)
            {
                newNum = 0;
            }
            else
            {
                newNum += 1;
            }
        }

        return newNum;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inRange = false;
        }
    }
}

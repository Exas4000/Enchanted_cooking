using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingStation : MonoBehaviour
{
    [SerializeField] Observer myObs;
    [SerializeField] Vector3[] recepeList; //argument 1 and 2 are the ingredients// argument 3 is the result
    [SerializeField] Inventory playerInventory;

    private Vector2 ingredients = new Vector2(-1, -1); //
    private int activeRecipe;
    private bool inRange = false;
    private float inputBuffer = 0;
    private float activeStateTimer = 0;
    [SerializeField] float cookingTimeInActive = 8f;

    //display
    [SerializeField] GameObject Ingredient_1;
    [SerializeField] GameObject Ingredient_2;
    [SerializeField] GameObject result;
    [SerializeField] GameObject timeIndicator;

    [SerializeField] GameObject crafting; //crafting ui
    [SerializeField] GameObject timer; //timer display and result

    [SerializeField] Animator spritesTimer;
    [SerializeField] Animator mySprites;

    private SoundFunctions mySounds;
    [SerializeField] AudioClip confirm;

    enum state
    {
        idle,
        menuMode,
        active,
        finished
    }
    private state myState = state.idle;

    private void Start()
    {
        mySounds = GetComponent<SoundFunctions>();
    }

    void Update()
    {
        //idle -> station not in use
        //menumode -> player is interacting with the station and should be unable to use movement
        //active -> the player is no longer unable to move and a recipe has been decided. passively goes toward the finished state
        //finished -> the station now act as a collect spot once for the crafted item going strait to idle state once done.

        progressTimers();

        switch (myState)
        {
            case state.idle:
                {
                    //open the crafting station if you have enough ingredients and are in range
                    if (playerInventory.GetNumItem() > 1 && InputManager.confim && inRange)
                    {
                        myState = state.menuMode;
                        myObs.CallMenu(true);

                        ingredients.x = 0;
                        crafting.SetActive(true);

                        mySounds.playSound(confirm);
                    }
                    return;
                }
            case state.menuMode:
                {

                    if (InputManager.pressQ )
                    {
                        //close crafting station
                        if (ingredients.y == -1)
                        {
                            myState = state.idle;
                            myObs.CallMenu(false);
                            ingredients = new Vector2(-1, -1);
                            crafting.SetActive(false);
                        }
                        else
                        {
                            //go back one step in the process of chosing ingredients
                            ingredients.y = -1;
                        }
                       
                    }

                    if (InputManager.confim)
                    {
                        //confirm
                        if (ingredients.y != -1)
                        {
                            if (checkForValidRecipe(playerInventory.GetItemLibraryID((int)ingredients.x) , playerInventory.GetItemLibraryID((int)ingredients.y)))
                            {
                                playerInventory.RemoveDuoItem(ingredients);
                                ingredients = new Vector2(-1, -1);
                                activeStateTimer = cookingTimeInActive;
                                myState = state.active;
                                crafting.SetActive(false);
                                timer.SetActive(true);
                                myObs.CallMenu(false);
                                spritesTimer.SetBool("Active", true);
                                mySounds.playSound(confirm);

                                if (mySprites != null)
                                {
                                    mySprites.SetBool("Active", true);
                                }
                            }
                            else
                            {
                                //Debug.LogWarning("no valid recipe");
                            }
                            //could add an else statement with sound effect/warning

                        }
                        else
                        {
                            while (ingredients.y < 0 || ingredients.y == ingredients.x)
                            {
                                ingredients.y++;
                            }
                            mySounds.playSound(confirm);
                        }
                    }

                    //changing the selection going toward first item
                    if (InputManager.pressLeft && inputBuffer <= 0)
                    {
                        if (ingredients.y != -1 && playerInventory.GetNumItem() > 2)
                        {
                            ingredients.y = DeterminePosition((int)ingredients.y, true);

                            if (ingredients.y == ingredients.x)
                            {
                                ingredients.y = DeterminePosition((int)ingredients.y, true);
                            }
                        }
                        else
                        {
                            ingredients.x = DeterminePosition((int)ingredients.x, true);                            
                        }

                        inputBuffer = 0.2f;
                    }

                    //changing the selection going toward the last item
                    if (InputManager.pressRight && inputBuffer <= 0)
                    {
                        if (ingredients.y != -1 && playerInventory.GetNumItem() > 2)
                        {
                            ingredients.y = DeterminePosition((int)ingredients.y, false);

                            if (ingredients.y == ingredients.x)
                            {
                                ingredients.y = DeterminePosition((int)ingredients.y, false);
                            }
                        }
                        else
                        {
                            ingredients.x = DeterminePosition((int)ingredients.x, false);
                        }

                        inputBuffer = 0.2f;

                    }

                    HandleStationDynamicElements(myState);



                    return;
                }
            case state.active:
                {
                    if (activeStateTimer <= 0)
                    {
                        
                        myState = state.finished;
                        spritesTimer.SetBool("Ended", true);

                        if (mySprites != null)
                        {
                            mySprites.SetBool("Ended", true);
                        }
                    }

                    HandleStationDynamicElements(myState);
                    return;
                }
            case state.finished:
                {
                    if (InputManager.confim && playerInventory.GetNumItem() < 6 && inRange)
                    {
                        playerInventory.AddItemToInventory((int)recepeList[activeRecipe].z);

                        spritesTimer.SetBool("Active", false);
                        spritesTimer.SetBool("Ended", false);
                        if (mySprites != null)
                        {
                            mySprites.SetBool("Active", false);
                            mySprites.SetBool("Ended", false);
                        }
                        timer.SetActive(false);

                        activeRecipe = 0;
                        myState = state.idle;
                    }
                        return;
                }
        }


    }

    private bool checkForValidRecipe(int itemX, int itemY)
    {
        bool isValid = false;

        List<Vector3> triage = new List<Vector3>();
        List<int> triageID = new List<int>();

        //Debug.Log("checking itemX " + itemX + " and ItemY " + itemY);

        for (int i = 0; i < recepeList.Length; i++)
        {
            if (recepeList[i].x == itemX || recepeList[i].y == itemX)
            {
                triage.Add(recepeList[i]);
                triageID.Add(i);

                //Debug.Log("added recipe to triage: "  + recepeList[i]);
            }
        }

        //Debug.Log(triage.Count + " recipe in triage");


        for (int i = 0; i < triage.Count; i++)
        {
            //is the recipe using 2 of the same?
            if(itemX == itemY && triage[i].x == triage[i].y && triage[i].x == itemX)
            {
                isValid = true;
                activeRecipe = triageID[i];
                return isValid;
            }
            else if (itemX != itemY) //&& ((int)triage[i].x == itemY || (int)triage[i].y == itemY)
            {
                //Debug.Log("something is right!");
                if  ((int)triage[i].x == itemY || (int)triage[i].y == itemY)
                {
                    Debug.Log("valid recipe");
                    isValid = true;
                    activeRecipe = triageID[i];
                    return isValid;
                }
                //Debug.Log("invalid recipe for triage(" + i + ")");
                
            }

        }

        return isValid;
    }

    private void progressTimers()
    {
        if (inputBuffer > 0)
        {
            inputBuffer -= Time.deltaTime;
        }

        if (activeStateTimer > 0)
        {
            activeStateTimer -= Time.deltaTime;
        }
    }

    private int DeterminePosition(int input,bool goToFirst)
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

    private void HandleStationDynamicElements(state _state)
    {
        switch(_state)
        {
            case state.menuMode:
                {
                    if (ingredients.x >-1)
                    {
                        Ingredient_1.SetActive(true);
                        Ingredient_1.GetComponent<SpriteRenderer>().sprite = playerInventory.GetItemSprite((int)ingredients.x);
                    }
                    else
                    {
                        Ingredient_1.SetActive(false);
                    }

                    if (ingredients.y > -1)
                    {
                        Ingredient_2.SetActive(true);
                        Ingredient_2.GetComponent<SpriteRenderer>().sprite = playerInventory.GetItemSprite((int)ingredients.y);
                    }
                    else
                    {
                        Ingredient_2.SetActive(false);
                    }

                    return;
                }
            case state.active:
                {
                    result.GetComponent<SpriteRenderer>().sprite = Item_Library.itemList[(int)recepeList[activeRecipe].z].GetMyImage();
                    return;
                }
        }
    }
}

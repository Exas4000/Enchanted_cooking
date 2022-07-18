using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer_simple : MonoBehaviour
{

    [SerializeField] float timePerSegment = 2f;
    [SerializeField] int numSegment = 8;
    private int startingSegment = 0;
    private Color timerColor = Color.green;
    [SerializeField] int[] itemId;
    [SerializeField] Observer myObs;
    [SerializeField] Animator myAnimator;
    [SerializeField] int numWants = 1;
    private List<Inv_Item> customerWants = new List<Inv_Item>();

    private float timer = 0;
    private bool inRange = false;
    private bool isTimerActive = true;
    private bool canSelfDestruct = false;
    private int spawnId = -1; //somehow, return this number to "Customer_Manager" through an observer call

    [SerializeField] GameObject inventory;
    [SerializeField] GameObject[] wantDisplay;
    [SerializeField] GameObject timerDisplay;
    /*
    //from monobehaviour already present in the gameobject
    public Customer_simple()
    {
        for (int i = 0; i < numWants; i++)
        {
            Debug.LogError("get item");
            int itemIdFromList = itemId[Random.Range(0, itemId.Length)];
            AddItemToWants(itemIdFromList);
        }
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numWants; i++)
        {
            int itemIdFromList = itemId[Random.Range(0, itemId.Length)];
            AddItemToWants(itemIdFromList);
        }
        timer = timePerSegment;

        if (inventory == null)
        {
            inventory = GameObject.FindGameObjectsWithTag("Inventory")[0];
        }

        if (myObs == null)
        {
            GameObject tempObs = GameObject.FindGameObjectsWithTag("Observer")[0];

            if (tempObs != null)
            {
                myObs = tempObs.GetComponent<Observer>();
            }
        }

        timerDisplay.GetComponent<SpriteRenderer>().color = timerColor;
        startingSegment = numSegment;
        myAnimator.SetBool("Active", true);
    }

    // Update is called once per frame
    void Update()
    {
        //do states later to make customers come and go into the restaurant. or just to have nice visuals for before/during/after their time waiting for food

        if (numSegment > 0)
        {
            if (isTimerActive)
            {
                timer -= Time.deltaTime;
            }
            
            if (timer <= 0)
            {
                numSegment -= 1;
                timer = timePerSegment;

                //changing the color of the timer!
                if (numSegment <= (startingSegment/2) )
                {
                    if (numSegment <= 2)
                    {
                        timerColor = Color.red;
                    }
                    else
                    {
                        timerColor = Color.yellow;
                    }

                    timerDisplay.GetComponent<SpriteRenderer>().color = timerColor;
                }
            }

            if (InputManager.confim && inRange)
            {
                checkForWants();
            }

            if (customerWants.Count == 0 && canSelfDestruct)
            {
                //Debug.Log("destroy self as i got no more wants");
                //observer call for satisfying a customer
                myObs.callCustomerRemoved(true,spawnId);
                //Destroy(this.gameObject);
                GetComponent<Sub_customers>().DestroySelfGameObject();
            }
        }
        else
        {
            //Debug.Log("destroy self as the player did not give me my stuff");

            ClearWants();

            myObs.callCustomerRemoved(false, spawnId);


            GetComponent<Sub_customers>().DestroySelfGameObject();
            //Destroy(this.gameObject);
        }
    }



    private void checkForWants()
    {
        if (inventory !=null)
        {
            Inventory interacteeInv = inventory.GetComponent<Inventory>(); //interacteeInv returns as null -> interactee is not the one with the Inventory monobehaviour
            Debug.Log(interacteeInv);
            List<int> invItemId = interacteeInv.returnItemIdList(); ///// current error! returning the list does not work///// see above

            for (int i = (invItemId.Count -1); i > -1; i--) //go from top to bottom to not screw up the list with RemoveSingleItem()
            {
                for (int j = 0; j < customerWants.Count; j++)
                {
                    if (invItemId[i] == customerWants[j].GetMyID())
                    {
                        //do both without breaking the 2 for loops.
                        // outer loop is more important than inner loop.
                        //doing the loop for one item at the time rather than doing everything at once keep both inventory safe.

                        j = customerWants.Count;

                        //remove want from customer
                        RemoveItemToWants(invItemId[i]);
                        //remove item from player inventory
                        interacteeInv.RemoveSingleItem(i);
                        
                    }
                }
            }

        }

    }

    private void HandleWantDisplay()
    {
        int i = 0;

        for (; i < 3 && i < customerWants.Count; i++)
        {
            wantDisplay[i].SetActive(true);
            wantDisplay[i].GetComponent<SpriteRenderer>().sprite = customerWants[i].GetMyImage();
            
        }

        for (; i < 3; i++)
        {
            wantDisplay[i].SetActive(false);
        }
    }

    public void AddItemToWants(int objectID)
    {


        if (objectID < Item_Library.itemList.Count)
        {
            customerWants.Add(Item_Library.itemList[objectID]);
        }
        HandleWantDisplay();
        canSelfDestruct = true;
    }

    public void RemoveItemToWants(Inv_Item itemToRemove)
    {
        customerWants.Remove(itemToRemove);
        HandleWantDisplay();
    }

    public void RemoveItemToWants(int itemIDToRemove)
    {
        for (int i = 0; i < customerWants.Count; i++)
        {
            if (itemIDToRemove == customerWants[i].GetMyID())
            {
                customerWants.RemoveAt(i);
                HandleWantDisplay();
                return;
            }
        }

    }

    public void ClearWants()
    {
        customerWants.Clear();
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

    public void SetSpawnId(int id)
    {
        spawnId = id;
    }

    public void EnableDisableTimer(bool isActive)
    {
        isTimerActive = isActive;
    }
}

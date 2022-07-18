using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    static public List<Inv_Item> playerInventory = new List<Inv_Item>();

    [SerializeField] GameObject inventoryDisplay;
    private bool isOpen = false;
    private bool update = false;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory.Clear();
    }

    // Update is called once per frame
    void Update()
    {

        //testing stuff for inventory



        if (inventoryDisplay != null & update)
        {
                           
            inventoryDisplay.GetComponent<Display_inventory>().updateDisplay(playerInventory);
            update = false;
           
        }
    }

    public void OpenCloseInventory()
    {
        if (isOpen)
        {
            inventoryDisplay.SetActive(false);
            isOpen = false;
        }
        else
        {
            inventoryDisplay.SetActive(true);
            isOpen = true;

            inventoryDisplay.GetComponent<Display_inventory>().updateDisplay(playerInventory);
        }
    }

    
    public void AddItemToInventory(int objectID)
    {
        if (objectID < Item_Library.itemList.Count && playerInventory.Count < 6)
        {
            playerInventory.Add(Item_Library.itemList[objectID]);
            inventoryDisplay.GetComponent<Display_inventory>().updateDisplay(playerInventory);
        }
        
    }

    public void RemoveSingleItem(int itemToRemove)
    {
        playerInventory.Remove(playerInventory[itemToRemove]);
        inventoryDisplay.GetComponent<Display_inventory>().updateDisplay(playerInventory);
    }

    public void RemoveDuoItem(Vector2 recipe)
    {
        Inv_Item itemX = playerInventory[(int)recipe.x];
        Inv_Item itemY = playerInventory[(int)recipe.y];

        playerInventory.Remove(itemX);
        playerInventory.Remove(itemY);
        inventoryDisplay.GetComponent<Display_inventory>().updateDisplay(playerInventory);
    }

    public int GetNumItem()
    {
        return playerInventory.Count;
    }

    public void visualUpdate()
    {
        inventoryDisplay.GetComponent<Display_inventory>().updateDisplay(playerInventory);
    }

    public Sprite GetItemSprite(int itemID)
    {
        return playerInventory[itemID].GetMyImage();
    }

    public int GetItemLibraryID(int itemID)
    {
        return playerInventory[itemID].GetMyID();
    }

    public List<int> returnItemIdList()
    {
        Debug.Log("returning the item list");
        List<int> idList = new List<int>();

        for (int i = 0; i < playerInventory.Count; i++)
        {
            idList.Add(playerInventory[i].GetMyID());
        }

        return idList;
    }
}

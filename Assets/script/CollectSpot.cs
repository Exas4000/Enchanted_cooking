using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectSpot : MonoBehaviour
{
    [SerializeField] int itemID = 0;
    [SerializeField] Inventory playerInventory;
    private bool inRange = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.pressSpace && inRange)
        {
            playerInventory.AddItemToInventory(itemID);
        }
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

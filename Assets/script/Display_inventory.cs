using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display_inventory : MonoBehaviour
{
    [SerializeField] Sprite defaultImage;

    [SerializeField] Text[] itemNames;
    [SerializeField] Image[] itemImages;


    public void updateDisplay(List<Inv_Item> _inventory)
    {
        for (int i = 0; i < itemImages.Length; i++)
        {
            if (i < _inventory.Count)
            {
                itemImages[i].sprite = _inventory[i].GetMyImage();

                itemNames[i].text = _inventory[i].GetMyName();
            }
            else
            {
                itemImages[i].sprite = defaultImage;

                itemNames[i].text = "";
            }

            
        }
    }
}

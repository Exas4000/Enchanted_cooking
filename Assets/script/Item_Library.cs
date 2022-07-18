using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Library : MonoBehaviour
{
    [SerializeField] Sprite[] itemPicture;
    [SerializeField] string[] itemName;
    public static List<Inv_Item> itemList = new List<Inv_Item>();

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        BuildLibrary();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BuildLibrary()
    {
        string nameInput = "default";

        for (int i = 0; i < itemPicture.Length; i++)
        {
            if (i >= itemName.Length)
            {
                nameInput = "default name";
            }
            else
            {
                nameInput = itemName[i];
            }

            itemList.Add(new Inv_Item(nameInput,i,itemPicture[i]));
        }
        
    }

}

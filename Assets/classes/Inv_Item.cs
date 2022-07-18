using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inv_Item
{
    private Sprite myImage;
    private string name;
    private int id;

    public Inv_Item(string _name, int _id,Sprite _image)
    {
        myImage = _image;
        name = _name;
        id = _id;
    }

    public Sprite GetMyImage()
    {
        return myImage;
    }

    public string GetMyName()
    {
        return name;
    }

    public int GetMyID()
    {
        return id;
    }
}

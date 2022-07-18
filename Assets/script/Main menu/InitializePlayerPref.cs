using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePlayerPref : MonoBehaviour
{
    static bool firstStartup = true;

    // Start is called before the first frame update
    void Start()
    {
        if (firstStartup)
        {
            InitPlayerPref();
        }
    }

    

    void InitPlayerPref()
    {
        Debug.Log("Creating playerPrefs");

        if (!PlayerPrefs.HasKey("Volume_Music"))
        {
            PlayerPrefs.SetFloat("Volume_Music", 1);
        }

        if (!PlayerPrefs.HasKey("Volume_Sound"))
        {
            PlayerPrefs.SetFloat("Volume_Sound", 1);
        }

    }
}

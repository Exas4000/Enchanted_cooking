using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFunctions : MonoBehaviour
{
    public AudioSource myself;
    public bool isMusic = false;
    public bool playOnAwake = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isMusic)
        {
            myself.volume = PlayerPrefs.GetFloat("Volume_Music");
        }
        else
        {
            myself.volume = PlayerPrefs.GetFloat("Volume_Sound");
            if (playOnAwake)
            {
                playSound(myself.clip);
            }
        }
    }

    public void playSound(AudioClip sound)
    {
        myself.clip = sound;
        myself.volume = PlayerPrefs.GetFloat("Volume_Sound");
        myself.Play();
    }
}

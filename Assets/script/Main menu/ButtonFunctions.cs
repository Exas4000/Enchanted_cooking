using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void modifyFloatPlayerpref(string playerPrefKey, float value)
    {
        if (PlayerPrefs.HasKey(playerPrefKey))
        {
            PlayerPrefs.SetFloat(playerPrefKey, value);
        }
        else
        {
            Debug.LogError("playerpref " + playerPrefKey + " does not exist");
        }
    }

    //version of the function for single target (Music)
    public void AudioSourceAdjustVolume(GameObject target)
    {
        target.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume_Music");
    }

    public void VerifyVolumeMusic( bool raise)
    {
        if (raise && PlayerPrefs.GetFloat("Volume_Music") < 1)
        {
            modifyFloatPlayerpref("Volume_Music", PlayerPrefs.GetFloat("Volume_Music") + 0.1f);
        }
        else if (PlayerPrefs.GetFloat("Volume_Music") > 0)
        {
            modifyFloatPlayerpref("Volume_Music", PlayerPrefs.GetFloat("Volume_Music") - 0.1f);
        }
    }

    public void VerifyVolumeSound(bool raise)
    {
        if (raise && PlayerPrefs.GetFloat("Volume_Sound") < 1)
        {
            modifyFloatPlayerpref("Volume_Sound", PlayerPrefs.GetFloat("Volume_Sound") + 0.1f);
        }
        else if (PlayerPrefs.GetFloat("Volume_Sound") > 0)
        {
            modifyFloatPlayerpref("Volume_Sound", PlayerPrefs.GetFloat("Volume_Sound") - 0.1f);
        }
    }
}

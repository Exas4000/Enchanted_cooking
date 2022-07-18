using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sub_GeneralScriptDisabler : Subscriber
{
    //used for non-instancialized gameobject to enable and disable specific scripts during cutscenes and other stuff

    [SerializeField] MonoBehaviour[] scripts;

    public override void OnCutscene()
    {
        base.OnCutscene();

        for (int i = 0; i < scripts.Length; i++)
        {
            scripts[i].enabled = false;
        }
    }

    public override void OnResumeFromCutscene()
    {
        base.OnResumeFromCutscene();
        for (int i = 0; i < scripts.Length; i++)
        {
            scripts[i].enabled = true;
        }
    }
}

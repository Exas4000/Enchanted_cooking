using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{

    private List<Subscriber> subcriberList = new List<Subscriber>();


    public void Subscribe(Subscriber newSub)
    {
        subcriberList.Add(newSub);
        Debug.Log(newSub + " added to subscriber list");
    }

    public void Unsub(Subscriber leavingSub)
    {
        subcriberList.Remove(leavingSub);
        Debug.Log(leavingSub + " removed from subscriber list");
    }

    public void CallPause(bool isPaused)
    {
        if (isPaused)
        {
            foreach (Subscriber sub in subcriberList)
            {
                sub.OnPause();
            }
        }
        else
        {
            foreach (Subscriber sub in subcriberList)
            {
                sub.OnResumeFromPause();
            }
        }
    }

    public void CallMenu(bool isMenu)
    {
        if (isMenu)
        {
            foreach (Subscriber sub in subcriberList)
            {
                sub.OnMenuActive();
                Debug.Log(sub + " used OnMenuActive");
            }
        }
        else
        {
            foreach (Subscriber sub in subcriberList)
            {
                sub.OnResumeFromMenuActive();
            }
        }
    }


    public void CallCutscene(bool isCut)
    {
        if (isCut)
        {
            foreach (Subscriber sub in subcriberList)
            {
                sub.OnCutscene();
            }
        }
        else
        {
            foreach (Subscriber sub in subcriberList)
            {
                sub.OnResumeFromCutscene();
            }
        }
    }

    public void callCustomerRemoved(bool isGoodOutcome, int positionId)
    {
        foreach (Subscriber sub in subcriberList)
        {
            sub.OnCustomerRemoved(isGoodOutcome, positionId);
        }
    }

}

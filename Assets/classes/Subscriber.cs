using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subscriber : MonoBehaviour
{

    [SerializeField] public Observer myObserver;


    public void Start()
    {
        if (myObserver == null)
        {
            GameObject tempObs = GameObject.FindGameObjectsWithTag("Observer")[0];
            
            if (tempObs != null)
            {
                myObserver = tempObs.GetComponent<Observer>();
                myObserver.Subscribe(this);
            }
        }
        else
        {
            myObserver.Subscribe(this);
        }
    }

    public void DestroySelfGameObject()
    {
        //necessity to not cause issue with observer

        if (myObserver !=null)
        {
            myObserver.Unsub(this);
        }

        Destroy(this.gameObject);
    }

    public virtual void OnPause()
    {

    }

    public virtual void OnCutscene()
    {

    }

    public virtual void OnMenuActive()
    {

    }

    public virtual void OnResumeFromPause()
    {

    }

    public virtual void OnResumeFromCutscene()
    {

    }

    public virtual void OnResumeFromMenuActive()
    {

    }

    public virtual void OnCustomerRemoved(bool isGoodOutcome, int positionId)
    {
        //goal is to pass information for score and the customer's position for smooth replacement
    }
}

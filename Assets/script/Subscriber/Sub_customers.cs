using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sub_customers : Subscriber
{
    Customer_simple myself;
    private void Awake()
    {
        myself = GetComponent<Customer_simple>();
    }

    public override void OnPause()
    {
        myself.EnableDisableTimer(false);
    }

    public override void OnResumeFromPause()
    {
        myself.EnableDisableTimer(true);
    }
}

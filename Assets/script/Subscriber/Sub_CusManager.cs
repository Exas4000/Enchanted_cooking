using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sub_CusManager : Subscriber
{
    Customer_Manager myself;

    private void Awake()
    {
        myself = GetComponent<Customer_Manager>();
    }

    public override void OnCustomerRemoved(bool isGoodOutcome, int positionId)
    {
        myself.ChangeNumCurrentCustomer(-1);
        myself.HandleSpawn(positionId, false);



        if (isGoodOutcome)
        {
            myself.ChangeScore(1);
        }
        else
        {
            myself.ChangePlayerLives(-1);
        }

        myself.checkForDifficultyCurve();

    }

    public override void OnCutscene()
    {
        myself.EnableDisableSpawn(false);
        base.OnCutscene();
    }

    public override void OnResumeFromCutscene()
    {
        myself.EnableDisableSpawn(true);
        base.OnResumeFromCutscene();
    }
}

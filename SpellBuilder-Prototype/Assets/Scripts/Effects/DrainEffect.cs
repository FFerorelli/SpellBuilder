using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainEffect : Effect
{
    //drain the channeled enemy's power

    [SerializeField] float drainAmount = 0.01f;
    public override void Activate()
    {
        spellManager.DrainPower(drainAmount);
    }
}

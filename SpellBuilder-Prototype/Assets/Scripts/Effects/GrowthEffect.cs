using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthEffect : Effect
{
    //drain the channeled enemy's power

    [SerializeField] float multiplier = 0.001f;
    public override void Activate()
    {
        spellManager.AddPower(spellManager.spellPower*multiplier);
    }
}

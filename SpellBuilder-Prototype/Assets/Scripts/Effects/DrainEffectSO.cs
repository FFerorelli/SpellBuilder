using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DrainEffectSO : EffectSO
{
    public FloatVariable drainPower;
    public override void Activate(SpellManager spellManager)
    {
        Debug.Log("Activating Drain effect");
    }
    public override void Deactivate(SpellManager spellManager)
    {
        Debug.Log("Deactivating Drain effect");
    }
    public override void Apply(SpellManager spellManager)
    {
        spellManager.DrainFromChannels(drainPower.value);
    }
}

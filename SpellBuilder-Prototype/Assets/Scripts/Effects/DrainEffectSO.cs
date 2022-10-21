using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DrainEffectSO : RecurrentEffectSO
{
    public float drainPower;
    SpellManager spellManager;

    public override void Activate(MonoBehaviour caller)
    {
        spellManager = caller.GetComponent<SpellManager>();
        if (caller == null)
        {
            Debug.Log("Missing SpellManager component");
            return;
        }
        base.Activate(caller);
    }

    public override void Apply(MonoBehaviour caller)
    {
        spellManager.DrainFromChannels(drainPower);
    }


}

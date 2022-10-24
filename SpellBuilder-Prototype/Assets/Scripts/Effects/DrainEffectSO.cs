using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Effects/SpellManagerEffects/DrainEffect")]
public class DrainEffectSO : RecurrentEffectSO<SpellManager>
{
    public float drainPower;

    public override EffectObject CreateEffect(MonoBehaviour caller)
    {
        return new DrainEffectObject(this, caller);
    }

}

public class DrainEffectObject : RecurrentEffectObject<SpellManager>
{
    protected float drainPower;

    public DrainEffectObject(EffectSO effectSO, MonoBehaviour caller) : base(effectSO, caller)
    {
        var castedEffectSO = (effectSO as DrainEffectSO);
        this.drainPower = castedEffectSO.drainPower;
    }

    public override void Apply()
    {
        targetComponent.DrainFromChannels(drainPower);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DrainEffectSO : RecurrentEffectSO
{
    public float drainPower;

    void Start()
    {
        if (maxApplications <= 0) maxApplications = -1;
    }

    public override EffectObject CreateEffect(MonoBehaviour caller)
    {
        return new DrainEffectObject(this, caller);
    }

}

public class DrainEffectObject : RecurrentEffectObject
{
    protected SpellManager spellManager;
    protected float drainPower;

    public DrainEffectObject(EffectSO effectSO, MonoBehaviour caller) : base(effectSO, caller)
    {
        var castedEffectSO = (effectSO as DrainEffectSO);
        this.drainPower = castedEffectSO.drainPower;
    }

    public override void Activate()
    {
        spellManager = caller.GetComponent<SpellManager>();
        if (spellManager == null)
            Debug.Log("EFFECT IS MISSING A SPELLMANAGER COMPONENT");
        base.Activate();
    }

    public override void Apply()
    {
        spellManager.DrainFromChannels(drainPower);
    }
}

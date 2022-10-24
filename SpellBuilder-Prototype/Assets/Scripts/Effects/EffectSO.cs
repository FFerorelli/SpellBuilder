using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EffectSO : ScriptableObject
{
    public string name = "BASIC EFFECT SCRIPTABLE OBJECT";
    public virtual EffectObject CreateEffect(MonoBehaviour caller)
    {
        return new EffectObject(this, caller);
    }
}

public class EffectObject
{
    public string name = "BASIC EFFECT OBJECT";
    protected EffectSO effectSO;
    protected MonoBehaviour caller;
    public bool isActive{get; set;}
    public EffectObject(EffectSO effectSO, MonoBehaviour caller)
    {
        this.caller = caller;
        this.effectSO = effectSO;
    }

    public virtual void Activate()
    {
        if (isActive)
            return;
        isActive = true;
    }

    public virtual void Deactivate()
    {
        if (!isActive)
            return;
        isActive = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Effects/NestedEffect")]
public class NestedEffectSO : RecurrentEffectSO<EffectManager>
{
    public EffectSO nestedEffect;

    public override EffectObject CreateEffect(MonoBehaviour caller)
    {
        return new NestedEffectObject(this, caller);
    }

}


public class NestedEffectObject : RecurrentEffectObject<EffectManager>
{
    protected EffectSO nestedEffect;

    public NestedEffectObject(EffectSO effectSO, MonoBehaviour caller) : base(effectSO, caller)
    {
        var castedEffectSO = (effectSO as NestedEffectSO);
        this.nestedEffect = castedEffectSO.nestedEffect;
    }

    public override void Apply()
    {
        targetComponent.AddEffect(nestedEffect);
    }
}

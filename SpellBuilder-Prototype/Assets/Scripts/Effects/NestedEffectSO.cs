using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Effects/EffectEffect")]
public class NestedEffectSO<T> : RecurrentEffectSO<EffectManager>
    where T : MonoBehaviour
{
    public RecurrentEffectSO<EffectManager> nestedEffect;

    public override EffectObject CreateEffect(MonoBehaviour caller)
    {
        return new NestedEffectObject(this, caller);
    }

}

public class NestedEffectObject<T> : RecurrentEffectObject<EffectManager>
    where T : MonoBehaviour
{
    protected RecurrentEffectSO<T> nestedEffect;

    public EffectEffectObject(EffectSO effectSO, MonoBehaviour caller) : base(effectSO, caller)
    {
        var castedEffectSO = (effectSO as NestedEffectSO<T>);
        this.nestedEffect = castedEffectSO.nestedEffect;
    }

    public override void Apply()
    {
        targetComponent.AddEffect(nestedEffect);
    }
}

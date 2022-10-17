using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EffectSO : ScriptableObject
{
    public bool isRecurrent;
    public float timeToWait;
    
    public virtual void Activate(SpellManager spellManager)
    {
        Debug.Log("Activating Empty effect");
    }
    public virtual void Deactivate(SpellManager spellManager)
    {
        Debug.Log("Deactivating Empty effect");
    }
    public virtual void Apply(SpellManager spellManager)
    {
        Debug.Log("Applying Empty effect");
    }
}

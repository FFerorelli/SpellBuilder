using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EffectManager : MonoBehaviour
{
    [SerializeField] public List<EffectSO> effectSOList;

    public void StartAllEffects()
    {
        for(var i = effectSOList.Count - 1; i >= 0; i--)
        {
            effectSOList[i].Activate(this);
        }
    }

    public void StopAllEffects()
    {
        for(var i = effectSOList.Count -1; i >= 0; i--)
        {
            Debug.Log("STOPPING EFFECT" + effectSOList[i].name);
            effectSOList[i].Deactivate(this);
        }
    }

    public void AddEffect(EffectSO effectSO)
    {
        effectSOList.Add(effectSO);
        effectSO.Activate(this);
    }

    public void RemoveEffect(EffectSO effectSO)
    {
        effectSOList.Remove(effectSO);
        effectSO.Deactivate(this);
    }

    void OnEnable()
    {
        StartAllEffects();
    }

    void OnDisable()
    {
        StopAllEffects();
    }

}
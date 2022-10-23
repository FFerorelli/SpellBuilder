using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EffectManager : MonoBehaviour
{
    [SerializeField] List<EffectSO> initialEffectsSOList;
    List<EffectObject> effectObjectList = new List<EffectObject>();

    void Start()
    {
        AddInitialEffects();
    }

    public void AddInitialEffects()
    {
        for(var i = initialEffectsSOList.Count - 1; i >= 0; i--)
        {
            AddEffect(initialEffectsSOList[i]);
        }
    }

    public void StartAllEffects()
    {
        for(var i = effectObjectList.Count - 1; i >= 0; i--)
        {
            effectObjectList[i].Activate();
        }
    }

    public void StopAllEffects()
    {
        for(var i = effectObjectList.Count -1; i >= 0; i--)
        {
            Debug.Log("STOPPING EFFECT" + effectObjectList[i].name);
            effectObjectList[i].Deactivate();
        }
    }

    public void AddEffect(EffectSO effectSO)
    {
        if (effectSO != null)
        {
            EffectObject effect = effectSO.CreateEffect(this);
            effectObjectList.Add(effect);
            effect.Activate();
        }            
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
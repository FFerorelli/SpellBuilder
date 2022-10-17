using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EffectManager : MonoBehaviour
{
    [SerializeField] public List<EffectSO> effectSOList;
    public List<Coroutine> coroutineList;
    SpellManager spellManager;
    
    void Awake()
    {
        spellManager = GetComponent<SpellManager>();
    }

    void Start()
    {
        StartAllEffects();
    }


    public void StartAllEffects()
    {
        foreach (EffectSO effectSO in effectSOList)
            ActivateEffect(effectSO);
    }

    public void AddEffect(EffectSO effectSO)
    {
        effectSOList.Add(effectSO);
        ActivateEffect(effectSO);
    }

    public void RemoveEffect(EffectSO effectSO)
    {
        effectSOList.Remove(effectSO);
        DeactivateEffect(effectSO);
    }

    public void ActivateEffect(EffectSO effectSO)
    {
        effectSO.Activate(spellManager);
        if (effectSO.isRecurrent)
            StartCoroutine(RecurringApplicator(effectSO));
    }

    public void DeactivateEffect(EffectSO effectSO)
    {
        if (effectSO.isRecurrent)
            StopCoroutine(RecurringApplicator(effectSO));
        effectSO.Deactivate(spellManager);

    }


    public void OnEnable()
    {
        StartAllEffects();
    }

    public void OnDisable()
    {
        foreach (EffectSO effectSO in effectSOList)
            DeactivateEffect(effectSO);
    }

    public IEnumerator RecurringApplicator(EffectSO effectSO) 
    {
        while(true)
        {
            effectSO.Apply(spellManager);
            yield return new WaitForSeconds(effectSO.timeToWait);
        }
    }

}
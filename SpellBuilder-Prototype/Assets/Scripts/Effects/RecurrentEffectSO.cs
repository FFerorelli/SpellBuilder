using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RecurrentEffectSO : EffectSO
{
    [SerializeField] public bool waitBeforeFirstApply;
    [SerializeField] public float timeBetweenApply;
    [SerializeField] public int maxApplications;

    
    public override EffectObject CreateEffect(MonoBehaviour caller)
    {
        return new RecurrentEffectObject(this, caller);
    }
}


public class RecurrentEffectObject : EffectObject
{
    protected bool waitBeforeFirstApply;
    protected int maxApplications;
    protected int remainingApplications;
    protected float timeBetweenApply;
    protected Coroutine gameCoroutine = null;
    
    public RecurrentEffectObject(EffectSO effectSO, MonoBehaviour caller) : base(effectSO, caller)
    {
        var castedEffectSO = (effectSO as RecurrentEffectSO);
        this.waitBeforeFirstApply = castedEffectSO.waitBeforeFirstApply;
        this.timeBetweenApply = castedEffectSO.timeBetweenApply;
        this.maxApplications = castedEffectSO.maxApplications;
        this.remainingApplications = castedEffectSO.maxApplications;
    }
    
    public override void Activate()
    {
        if (gameCoroutine == null)
            gameCoroutine = caller.StartCoroutine(RecurrentApply());

        Debug.Log(gameCoroutine);
        base.Activate();

    }
    public override void Deactivate()
    {
        base.Deactivate();
        if (gameCoroutine != null)
        {
            caller.StopCoroutine(gameCoroutine);
            gameCoroutine = null;
        }
    }

    public IEnumerator RecurrentApply()
    {
        if (waitBeforeFirstApply)
        {         
            yield return new WaitForSeconds(timeBetweenApply);
            waitBeforeFirstApply = false;
        }

        while(remainingApplications > 0 || remainingApplications < 0)
        {
            Debug.Log(caller + " has " + remainingApplications + " remaining drains");
            Apply();
            remainingApplications -=1;
            yield return new WaitForSeconds(timeBetweenApply);
        }
        Deactivate();
    }

    public virtual void Apply()
    {
        Debug.Log("Applying Unspecified Recurrent Effect");
    }
}

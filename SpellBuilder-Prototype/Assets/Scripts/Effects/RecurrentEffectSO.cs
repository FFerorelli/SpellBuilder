using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecurrentEffectSO<TargetComponent> : EffectSO
    where TargetComponent : MonoBehaviour
{
    [SerializeField] public bool waitBeforeFirstApply;
    [SerializeField] public float timeBetweenApply;
    [SerializeField] public float deactivationTime;
    [SerializeField] public int maxApplications;

    
    public override EffectObject CreateEffect(MonoBehaviour caller)
    {
        return new RecurrentEffectObject<TargetComponent>(this, caller);
    }
}


public class RecurrentEffectObject<TargetComponent> : EffectObject
    where TargetComponent : MonoBehaviour
{
    protected bool waitBeforeFirstApply;
    protected int maxApplications;
    protected float timeBetweenApply;
    protected float deactivationTime;

    protected int remainingApplications;
    protected Coroutine gameCoroutine = null;
    protected TargetComponent targetComponent;


    public RecurrentEffectObject(EffectSO effectSO, MonoBehaviour caller) : base(effectSO, caller)
    {
        var castedEffectSO = (effectSO as RecurrentEffectSO);
        this.waitBeforeFirstApply = castedEffectSO.waitBeforeFirstApply;
        this.timeBetweenApply = castedEffectSO.timeBetweenApply;
        this.maxApplications = castedEffectSO.maxApplications;
        this.remainingApplications = castedEffectSO.maxApplications;
        this.deactivationTime = castedEffectSO.deactivationTime;

        this.targetComponent = caller.GetComponent<TargetComponent>();
        if (targetComponent == null)
        {
            Debug.Log("WARNING: EFFECT TARGET IS MISSING THE REQUIRED COMPONENT");
        }

    }
    
    public override void Activate()
    {
        if (gameCoroutine == null)
            gameCoroutine = caller.StartCoroutine(RecurrentApply());
        if (deactivationTime > 0)
            caller.StartCoroutine(StopAfterSeconds(deactivationTime));
        base.Activate();

    }
    public override void Deactivate()
    {
        if (gameCoroutine != null)
        {
            caller.StopCoroutine(gameCoroutine);
            gameCoroutine = null;
        }
        base.Deactivate();

    }

    public IEnumerator StopAfterSeconds(float timeBeforeStop)
    {
        yield return new WaitForSeconds(timeBeforeStop);
        Deactivate();
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

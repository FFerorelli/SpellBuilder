using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RecurrentEffectSO : EffectSO
{
    public bool isRecurrent;
    public float timeToWait;
    private Coroutine gameCoroutine = null;
    //WATCH OUT THIS WILL BREAK IF WE USE THESE EFFECT FOR MULTIPLE ENEMIES

    
    public override void Activate(MonoBehaviour caller)
    {
        base.Activate(caller);
        if (gameCoroutine == null)
            gameCoroutine = caller.StartCoroutine(RecurrentApply(caller));
    }
    public override void Deactivate(MonoBehaviour caller)
    {
        base.Deactivate(caller);
        caller.StopCoroutine(gameCoroutine);
        gameCoroutine = null;
    }

    public IEnumerator RecurrentApply(MonoBehaviour caller)
    {
        while(true)
        {
            Apply(caller);
            yield return new WaitForSeconds(timeToWait);
        }
    }

    public virtual void Apply(MonoBehaviour caller)
    {
        Debug.Log("Applying Unspecified Recurrent Effect");
    }
}

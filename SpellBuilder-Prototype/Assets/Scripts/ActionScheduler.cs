using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScheduler : MonoBehaviour
{

    IAction currentAction;
    // Start is called before the first frame update
    public void StartAction(IAction action)
    {
        if (currentAction == action) return;

        if (currentAction != null)
        {
            currentAction.Cancel();
        }
        // print("starting " + currentAction);
        currentAction = action;
    }
    public void CancelCurrentAction()
    {
        StartAction(null);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

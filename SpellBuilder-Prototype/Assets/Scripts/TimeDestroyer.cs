using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroyer : MonoBehaviour
{
    public float TimeToWait;

    void Start()
    {
        Destroy(gameObject, TimeToWait);
    }
}

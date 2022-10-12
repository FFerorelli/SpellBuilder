using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameObject currentTarget;
    LineRenderer channelRenderer;
    bool isChanneling;
    // Start is called before the first frame update
    void Start()
    {
        channelRenderer = GetComponent<LineRenderer>();
       // isChanneling = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

}

using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public SpellManager spellManager;
    
    void Start()
    {
        spellManager = GetComponent<SpellManager>();
    }

    void Update()
    {
        Activate();
    }

    public virtual void Activate(){}
    public virtual void OnChannelCreated(){}
    public virtual void OnSpellDestroy(){}
}
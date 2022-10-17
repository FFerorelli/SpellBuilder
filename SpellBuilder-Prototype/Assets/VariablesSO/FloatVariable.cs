using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatVariable : ScriptableObject
{
    
    public float value;
    public string Name;
    void OnValidate()
    {
        this.name = Name;
    }
    
}

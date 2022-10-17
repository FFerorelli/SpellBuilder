using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpellTypeVariable : ScriptableObject
{
    
    public SpellType type;
    public string Name;
    void OnValidate()
    {
        this.name = Name;
    }
    
}

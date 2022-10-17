using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpellTypes", fileName = "unnamed spelltype")]
public class SpellType : ScriptableObject
{
    public string type;
    public SpellType inverse;
}
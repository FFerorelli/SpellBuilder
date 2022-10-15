using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum SpellStatus
{
    IDLE,
    BUILDING
}

public class SpellManager : MonoBehaviour
{
    ChannelManager channelManager;
    public SpellStatus spellStatus = 0;
    public SpellType spellType = null;
    public float spellPower = 0;

    void Start()
    {
        Initialize();
    }

    public float GetPower()
    {
        if (spellStatus == 0)
            return 0;
        else
            return spellPower;
    }

    public void CreateSpell(SpellType spellType = null)
    {
        this.spellPower = 0;
        this.spellType = spellType;
        this.spellStatus = SpellStatus.BUILDING;
        Debug.Log($"Created a {spellType} spell of {spellPower} power");
    }

    public void SpellCancel()
    {
        Initialize();
    }

    public void Initialize()
    {
        spellStatus = 0;
        spellPower = 0;
    }

    public void ChannelInterrupted()
    {

    }

    public bool IsChannelable()
    {
        return true;
    }

    public void InvertSpellType()
    {
        spellType = spellType.inverse;
    }

    public void RevertChannel()
    {
        Debug.Log("REVERTING A CHANNEL (TODO)");
    }

    public float AddPower(ManaPool pool, bool ignoreType = false)
    {
        return AddPower(pool.amount, pool.type, ignoreType);
    }

    public float AddPower(float amount, SpellType incomingType = null, bool ignoreType = false)
    {
        if (spellStatus == SpellStatus.IDLE)
        {
            CreateSpell(incomingType);
        }

        if (ignoreType || incomingType == this.spellType)
            {
                spellPower += amount;
                return amount;
            }
        else if (incomingType == null)
            {
                Debug.Log($"Trying to add NULL power to a {spellType} spell");
                return 0;
            }
        else
            {
                Debug.Log($"Trying to add {incomingType} power to a {spellType} spell");
                return 0;
            }
    }

}


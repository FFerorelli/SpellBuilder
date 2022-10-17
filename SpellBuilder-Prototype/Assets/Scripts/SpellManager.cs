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
    public SpellStatus spellStatus = 0;
    public SpellTypeVariable spellType;
    public FloatVariable spellPower;

    ChannelManager channelManager;


    void Awake()
    {
        channelManager = GetComponent<ChannelManager>();
    }

    void Start()
    {
        Initialize();
    }

    public float GetPower()
    {
        if (spellStatus == 0)
            return 0;
        else
            return spellPower.value;
    }

    public void CreateSpell(SpellType spellType = null)
    {
        this.spellPower.value = 0;
        this.spellType.type = spellType;
        this.spellStatus = SpellStatus.BUILDING;
        Debug.Log($"Created a {spellType.type} spell of {spellPower.value} power");
    }

    public void SpellCancel()
    {
        Initialize();
    }

    public void Initialize()
    {
        spellStatus = 0;
        spellPower.value = 0;
        spellType.type = null;
    }

    public void ChannelInterrupted()
    {

    }

    public void DrainFromChannels(float amountPerChannel)
    {
        if (channelManager != null)
            channelManager.DrainFromChannels(amountPerChannel);
    }

    public bool IsChannelable()
    {
        return true;
    }

    public void InvertSpellType()
    {
        spellType.type = spellType.type.inverse;
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

        if (ignoreType || incomingType == this.spellType.type)
            {
                spellPower.value += amount;
                return amount;
            }
        else if (incomingType == null)
            {
                Debug.Log($"Trying to add NULL power to a {spellType.type} spell");
                return 0;
            }
        else
            {
                Debug.Log($"Trying to add {incomingType} power to a {spellType.type} spell");
                return 0;
            }
    }

}


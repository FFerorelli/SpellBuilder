using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SpellType
{
    BASIC,
    SAME,
    FIRE,
    WATER
}

public enum SpellStatus
{
    IDLE,
    BUILDING
}

public class SpellManager : MonoBehaviour
{
    Channel channel;
    IChannelable target;

    public SpellStatus spellStatus = 0;
    public SpellType spellType = 0;
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

    public void CreateSpell(SpellType spellType = 0)
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
        target = null;
        spellStatus = 0;
        spellPower = 0;
    }

    public bool ChannelAttach(Channel channel, IChannelable target)
    {
        this.target = target;
        this.channel = channel;
        if (spellStatus == 0)
            CreateSpell(target.GetSpellType());
        return true;
    }

    public void ChannelInterrupted()
    {

    }

    public bool IsChannelable()
    {
        return true;
    }

    public void DrainPower(float amount)
    {
        float obtained = channel.DrainPower(amount);
        SpellType type = channel.GetTargetedType();
        AddPower(obtained, type);
    }

    public float AddPower(float amount, SpellType incomingType = SpellType.SAME)
    {
        if (spellStatus == 0)
            return 0;
        if (incomingType == SpellType.SAME)
            {
                spellPower += amount;
                return amount;
            }
        else if (incomingType == this.spellType)
            {
                spellPower += amount;
                return amount;
            }
        else
            {
                Debug.Log($"Trying to add {incomingType} power to a {spellType} spell");
                return 0;
            }
    }

}


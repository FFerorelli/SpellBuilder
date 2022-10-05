using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
    public float drainPower = 0.01f;
    float power_points = 0;
    
    Channel channel;
    SpellType spellType;
    IChannelable target;

    public void AttachChannel(Channel channel, IChannelable target)
    {
        spellType = target.GetSpellType();
        this.target = target;
        this.channel = channel;
    }

    public bool IsChannelable()
    {
        return true;
    }

    public void ChannelInterrupted()
    {
        Debug.Log("Channel Interrupted for this spell");
        target = null;
        channel = null;
        spellType = SpellType.BASIC;
    }


    public void AddPower(float amount)
    {
        power_points += amount;
    }


    public void Decrease(float decrease)
    {
        power_points = Mathf.Min(0, power_points - decrease);
        if (power_points == 0)
        {
            Extinguish();
        }

    }

    public void Extinguish()
    {
        Debug.Log($"Spell of type {spellType} went extinguished");
    }

    public void Release()
    {
        Debug.Log($"Releasing {power_points} points of type {spellType}");
    }
}

public enum SpellType
{
    BASIC,
    FIRE,
    WATER
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SpellType
{
    BASIC,
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
    [SerializeField] float basicDrainPower = 0.01f;
    [SerializeField] float basicPowerPoints = 0;
    SpellStatus spellStatus = 0;

    [SerializeField] float drainPower;
    [SerializeField] float powerPoints;
    [SerializeField] SpellType spellType;

    void Start()
    {
        SpellInit();
    }

    public float GetPower()
    {
        return powerPoints;
    }

    public float GetDrainPower()
    {
        return drainPower;
    }

    public void CreateSpell(SpellType spellType = 0)
    {
        this.powerPoints = basicPowerPoints;
        this.drainPower = basicDrainPower;
        this.spellType = spellType;
        spellStatus = SpellStatus.BUILDING;
        Debug.Log($"Created a {spellType} spell of {powerPoints} power");
    }

    public void ReleaseSpell()
    {
        Debug.Log($"Releasing a {spellType} spell of {powerPoints} power");
        SpellInit();
    }

    public void SpellInit()
    {
        target = null;
        drainPower = 0;
        powerPoints = 0;
        spellType = 0;
        spellStatus = 0;
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

    public void AddPower(float amount, SpellType spellType)
    {
        powerPoints += amount;
        //manage spell type
    }

}


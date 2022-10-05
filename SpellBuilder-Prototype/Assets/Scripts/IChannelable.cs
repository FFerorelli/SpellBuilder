using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChannelable
{
    GameObject GetGameObject();
    SpellType GetSpellType();
    float DrainPower(float amount);
    void SetChannel(Spell spell, Channel channel);
    void ChannelInterrupted();
    bool IsChannelable();
}

public enum ChannelState
{
    CHANNELING,
    IDLE
}

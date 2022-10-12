using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChannelable
{
    GameObject GetGameObject();
    SpellType GetSpellType();
    float DrainPower(float amount);
    bool ChannelAttach(Channel channel);
    void ChannelInterrupted();
    bool IsChannelable();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChannelable
{
    GameObject GetGameObject();
    Vector2 GetPosition();
    SpellType GetSpellType();
    float DrainPower(float amount);
    bool ChannelAttach(Channel channel);
    bool IsChannelable();
}

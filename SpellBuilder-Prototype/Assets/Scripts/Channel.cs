using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum ChannelState
{
    ATTACHED,
    CHANNELING,
    IDLE
}

public struct ManaPool
{
    public float amount;
    public SpellType type;

    public ManaPool(float amount, SpellType type)
    {
        this.amount = amount;
        this.type = type;
    }
}

public class Channel : MonoBehaviour
{
    [SerializeField] public ChannelState channelState;
    [SerializeField] IChannelable target;
    Vector2 sourcePoint;
    ChannelManager channelManager;

    public void Initialize(ChannelManager manager, Vector2 sourcePoint)
    {
        channelManager = manager;
        channelManager.RegisterChannel(this);

        this.sourcePoint = sourcePoint;
        target = null;
        channelState = ChannelState.IDLE;
    }

    void OnDestroy() => channelManager.DeregisterChannel(this);

    void Update()
    {
        if (channelState == ChannelState.ATTACHED)
            if (!ChannelCheck())
                Detach();
    }

    public ManaPool DrainFromTarget(float amount)
    {
        if (channelState != ChannelState.ATTACHED)
            return new ManaPool(0, null);
        float obtained = target.DrainPower(amount);
        return new ManaPool(obtained, target.GetSpellType());
    }

    public bool ChannelCheck()
    {
        if (target == null) 
            return false;
        if (!target.IsChannelable())
            return false;
        return true;
    }

    public void Detach()
    {
        Debug.Log("Destroying channel with" + target?.GetGameObject().name);
        Destroy(this.gameObject);
    }

    public bool Attach(IChannelable newTarget)
    {

        if (!newTarget.ChannelAttach(this))
        {
            Debug.Log("Failed to attach to target");
            Detach();
            return false;
        }
        
        channelState = ChannelState.ATTACHED;
        this.target = newTarget;
        return true;
    }

    public Vector2 GetEndpoint(int endpoint = 0)
    {
        if (endpoint == 0)
            return sourcePoint;
        else
            return target.GetPosition();
    }




}

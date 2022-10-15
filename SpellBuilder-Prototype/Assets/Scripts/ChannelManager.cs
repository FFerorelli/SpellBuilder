using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChannelManagerState
{
    TARGETING,
    IDLE
}

public class ChannelManager : MonoBehaviour
{
    [SerializeField] SpellManager spellManager;
    [SerializeField] GameObject sourceObject;
    [SerializeField] GameObject channelPrefab;
    [SerializeField] ChannelManagerState channelManagerState;
    [SerializeField] float drainAmountForChannel;

    HashSet<Channel> channelList = new HashSet<Channel>();
    public void RegisterChannel(Channel channel) => channelList.Add(channel);
    public void DeregisterChannel(Channel channel) => channelList.Remove(channel);


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (channelManagerState == ChannelManagerState.TARGETING)
        {
            if (Input.GetMouseButtonUp(0))
            {
                IChannelable temp_target = GetChannelableAtMousePos();
                MakeNewChannel(temp_target);
            }
        }
        DrainFromChannels(drainAmountForChannel);

    }

    public void DrainFromChannels(float amountForChannel)
    {
        foreach (Channel channel in channelList)
        {
            ManaPool obtained = channel.DrainFromTarget(amountForChannel);
            spellManager.AddPower(obtained);
        }
    }

    private bool MakeNewChannel(IChannelable target)
    {
        if (target == null)
            return false;
        if (!target.IsChannelable())
            return false;
        //instantiate a new channel as a child of this;
        GameObject newChannel = Instantiate(channelPrefab, new Vector3(0, 0, 0), Quaternion.identity, this.transform);
        Channel channel = newChannel.GetComponent<Channel>();
        channel.Initialize(this, sourceObject.transform.position);
        channel.Attach(target);
        return true;
    }


    private IChannelable GetChannelableAtMousePos()
    {     
        //return clicked IChannelable
        RaycastHit2D hit;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null) 
        {
            Debug.Log("Channelable object was clicked!" + hit.collider.name);
            return (hit.collider.gameObject.GetComponent<IChannelable>() as IChannelable);
        }
        else return null;
    }

}


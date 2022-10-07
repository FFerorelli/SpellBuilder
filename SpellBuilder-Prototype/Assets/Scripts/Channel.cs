using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ChannelState
{
    CHANNELING,
    TARGETING,
    IDLE
}

public class Channel : MonoBehaviour
{
    IChannelable target;
    SpellManager spellManager;
    LineRenderer channelRenderer;
    ChannelState channelState;
    RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {
        channelState = ChannelState.TARGETING;
        channelRenderer = GetComponent<LineRenderer>();
        spellManager = GetComponent<SpellManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (channelState == ChannelState.TARGETING)
        {
            if (Input.GetMouseButtonDown(0))
            {
                IChannelable temp_target = Get_Channelable();
                if (temp_target != null)
                {
                    ChannelSetup(temp_target);
                }
            }
        }

        if (channelState == ChannelState.CHANNELING)
        {
            if (ChannelCheck())
            {
                DrainPower();
                RenderChannelUpdate(target);
            }
            else
            {
                ChannelDestroy();
            }
        }
    }

    public bool ChannelCheck()
    {
        if (target == null)
            return false;
        if (!target.IsChannelable())
            return false;
        if (!spellManager.IsChannelable())
            return false;
        return true;
    }

    public void ChannelDestroy()
    {
        target?.ChannelInterrupted();
        target = null;
        spellManager.ChannelInterrupted();
        RenderChannelDestroy();
        channelState = ChannelState.TARGETING;
        Debug.Log("CHANNEL DESTROYED");
    }

    private void ChannelSetup(IChannelable new_target)
    {

        if (!spellManager.ChannelAttach(this, new_target))
        {
            Debug.Log("Failed to attach to spell");
            ChannelDestroy();
            return;
        }
        if (!new_target.ChannelAttach(this))
        {
            Debug.Log("Failed to attach to target");
            ChannelDestroy();
            return;
        }
        
        target = new_target;
        channelState = ChannelState.CHANNELING;
        RenderChannelingSetup(target);

        Debug.Log("Started channel");
    }

    private IChannelable Get_Channelable()
    {     
        //return clicked IChannelable
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

    private void RenderChannelUpdate(IChannelable target)
    {
        channelRenderer.SetPosition(0, transform.position);
        channelRenderer.SetPosition(1, target.GetGameObject().transform.position);
    }

    private void RenderChannelingSetup(IChannelable target)
    {
        channelRenderer.enabled = true;
        RenderChannelUpdate(target);
    }

    private void RenderChannelDestroy()
    {
        channelRenderer.enabled = false;
    }

    public void DrainPower()
    {
        float amount = spellManager.GetDrainPower();
        float power = target.DrainPower(amount);
        spellManager.AddPower(power, target.GetSpellType());     
    }

}


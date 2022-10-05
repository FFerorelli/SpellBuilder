using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Channel : MonoBehaviour
{
    IChannelable target;
    Spell currentSpell;
    LineRenderer channelRenderer;
    ChannelState channelState;
    RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {
        channelState = ChannelState.IDLE;
        channelRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (channelState == ChannelState.IDLE)
        {
            if (Input.GetMouseButtonDown(0))
            {
                IChannelable temp_target = Get_Channelable();
                if (temp_target != null)
                {
                    if (temp_target.IsChannelable())
                        ChannelSetup(temp_target);
                }
            }
        }

        if (channelState == ChannelState.CHANNELING)
        {
            if (ChannelCheck())
            {
                DrainPower(target,currentSpell);
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
        if (currentSpell == null)
            return false;
        if (!currentSpell.IsChannelable())
            return false;
        return true;
    }

    public void ChannelDestroy()
    {
        if (target != null) target.ChannelInterrupted();
        target = null;
        if (currentSpell != null) currentSpell.ChannelInterrupted();
        currentSpell = null;
        RenderChannelDestroy();
        channelState = ChannelState.IDLE;
        Debug.Log("CHANNEL DESTROYED");
    }

    private void ChannelSetup(IChannelable new_target)
    {
        target = new_target;
        channelState = ChannelState.CHANNELING;
        RenderChannelingSetup(target);
        SpellSetup(target);
        target.SetChannel(currentSpell, this);
        Debug.Log("Started channel");
    }

    private void SpellSetup(IChannelable target)
    {
        currentSpell = new Spell();
        currentSpell.AttachChannel(this,target);
    }

    private IChannelable Get_Channelable()
    {     
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

    void DrainPower(IChannelable target, Spell spell)
    {
        float amount = spell.drainPower;
        float power = target.DrainPower(amount);
        spell.AddPower(power);     
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public Interface IChannelable
{
    SpellType GetSpelltype();
    float DrainPower(float amount);
    void SetChannel(Spell spell, Channel channel);
    void ChannelInterrupted();
}

public enum ChannelState
{
    CHANNELING,
    IDLE
}

public class Channel : MonoBehaviour
{
    IChannelable target;
    Spell current_spell;
    LineRenderer channelRenderer;
    ChannelState channelState;
    RaycastHit2D hit;
    float elaspedTime = 0;
    float startTime = 0;
    float powerTimer = 1;
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
                    ChannelSetup(temp_target);
                }
            }
        }

        if (channelState == ChannelState.CHANNELING)
        {
            DrainPower(target,current_spell);
            UpdateChannelRendered();
        }
    }

    public void ChannelDestroy()
    {
        if (target != null) target.ChannelInterrupted();
        if (spell != null) spell.ChannelInterrupted();
        RenderChannelDestroy();
        channelState = ChannelState.IDLE;
    }

    private void DrainPower(IChannelable target, Spell spell)
    {
        Debug.Log("DRAIN POWER(target,spell)")
    }

    private ChannelSetup(IChannelable new_target)
    {
        target = new_target;
        channelState = ChannelState.CHANNELING;
        RenderChannelingSetup(target);
        SpellSetup(target);
        target.SetChannel(currentSpell, this);
        Debug.Log($"Started channel on {target.collider.name}");
    }

    private SpellSetup(IChannelable target)
    {
        currentSpell = new Spell(this, target, target.GetSpellType());
    }

    private IChannelable Get_Channelable()
    {     
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null) 
        {
            Debug.Log("Channelable object was clicked!" + hit.collider.name);
            return hit.collider.gameObject as IChannelable;
        }
    }

    private void RenderChannelUpdate(GameObject target)
    {
        channelRenderer.SetPosition(0, transform.position);
        channelRenderer.SetPosition(1, target.transform.position);
    }

    private void RenderChannelingSetup(GameObject target)
    {
        channelRenderer.enabled = true;
        channelRenderer.SetPosition(0, transform.position);
        channelRenderer.SetPosition(1, target.transform.position);
    }

    private void RenderChannelDestroy()
    {
        channelRender.enabled = false;
    }

    void DrainPower(IChannelable target, Spell spell)
    {
        float amount = spell.GetDrainPower();
        float power = target.DrainPower(amount);
        spell.AddPower(power);     
    }

}


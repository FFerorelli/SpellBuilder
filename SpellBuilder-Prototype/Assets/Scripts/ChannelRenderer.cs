using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Channel))]
[RequireComponent(typeof(LineRenderer))]
public class ChannelRenderer : MonoBehaviour
{
    [SerializeField] Channel channel;
    LineRenderer lineRenderer;
    RaycastHit2D hit;

    void Start()
    {
        channel = GetComponent<Channel>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (channel.channelState == ChannelState.ATTACHED)
            RenderChannelUpdate();
    }

    void OnDisable()
    {
        if (lineRenderer != null)
            lineRenderer.enabled = false;
    }

    void OnEnable()
    {
        if (lineRenderer != null)
            lineRenderer.enabled = true;
    }

    private void RenderChannelUpdate()
    {
        lineRenderer.SetPosition(0, channel.GetEndpoint(0));
        lineRenderer.SetPosition(1, channel.GetEndpoint(1));
    }

    private void RenderChannelSetup()
    {
        lineRenderer.enabled = true;
        RenderChannelUpdate();
    }

}

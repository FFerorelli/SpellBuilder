using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Channel : MonoBehaviour
{
    GameObject target;
    LineRenderer channelRenderer;
    Vector2 dir;
    RaycastHit2D hit;
    Power power;
    float elaspedTime = 0;
    float startTime = 0;
    float powerTimer = 1;
    // Start is called before the first frame update
    void Start()
    {
        channelRenderer = GetComponent<LineRenderer>();
        power = GetComponent<Power>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {         
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            Debug.Log("Something was clicked!" + hit.collider.name);
        }
        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
            RenderChanneling(target);
            DrainPower();
        }
    }

    private void RenderChanneling(GameObject target)
    {
        dir = hit.collider.transform.position - transform.position;
        channelRenderer.enabled = true;
        channelRenderer.SetPosition(0, transform.position);
        channelRenderer.SetPosition(1, target.transform.position);
    }

    void DrainPower()
    {
        powerTimer -= Time.deltaTime;
        
        if (powerTimer < 0)
        {
            powerTimer = 1;
            power.points += 1;
        }
    }

}


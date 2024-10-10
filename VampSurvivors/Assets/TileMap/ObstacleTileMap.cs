using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTileMap : MonoBehaviour
{
    bool eventStarted = false;

    private void Start()
    {
        EventManager.Instance.OnStartLadderEvent += StartEvent;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnStartLadderEvent -= StartEvent;
    }

    void StartEvent()
    {
        eventStarted = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (eventStarted)
            return;

        Player p = collision.GetComponentInParent<Player>();
        if (!p)
            return;
        p.AllowInterraction(true);        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player p = collision.GetComponentInParent<Player>();
        if (!p)
            return;
        p.AllowInterraction(false);
    }
}

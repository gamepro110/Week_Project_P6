using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePickup : MonoBehaviour
{
    protected Player m_player;

    private void Start()
    {
        EventManager.ResetLevelEvent += ResetItem;
        m_player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == m_player.gameObject)
        {
            DoPickup();
            gameObject.SetActive(false);
        }
    }

    protected abstract void DoPickup();

    protected abstract void ResetItem();

    protected abstract void OnDestroy();
}
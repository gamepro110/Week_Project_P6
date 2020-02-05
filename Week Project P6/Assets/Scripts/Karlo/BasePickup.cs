using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    Trap,
}

public class BasePickup : MonoBehaviour
{
    protected Player m_player;

    private void Start()
    {
        m_player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == m_player.gameObject)
        {
            DoPickup();
            gameObject.SetActive(false);
        }
    }

    protected virtual void DoPickup()
    {
    }
}
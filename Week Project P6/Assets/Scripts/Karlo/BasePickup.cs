using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    Trap,
}

public class BasePickup : MonoBehaviour
{
    [SerializeField] protected Player m_player;

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
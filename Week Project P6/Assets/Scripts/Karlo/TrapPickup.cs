using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPickup : BasePickup
{
    [SerializeField] private List<Trap> m_traps = null;

    protected override void DoPickup()
    {
        m_player.PickupTrap(m_traps[Random.Range(0, m_traps.Count)]);
    }

    protected override void ResetItem()
    {
        gameObject.SetActive(true);
    }

    protected override void OnDestroy()
    {
        EventManager.ResetLevelEvent -= ResetItem;
    }
}
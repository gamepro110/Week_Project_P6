using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPickup : BasePickup
{
    [SerializeField] private Trap m_trap;

    protected override void DoPickup()
    {
        m_player.PickupTrap(m_trap);
    }
}
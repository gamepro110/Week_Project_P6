using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotTrap : Trap
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == m_loli.gameObject)
        {
            Debug.Log("you hit a loli, kono hentai");
            //TODO wait for ai to work?
        }
        else if (collision.gameObject == m_player.gameObject)
        {
            m_player.ResetVelocity();
            StartCoroutine(m_player.SlowEffect(1f));
            m_player.Invincible = true;
            m_invincibleRoutine = StartCoroutine(m_player.Flicker());
            StartCoroutine(WaitForFlicker());
        }

        base.OnTriggerEnter2D(collision);
    }
}
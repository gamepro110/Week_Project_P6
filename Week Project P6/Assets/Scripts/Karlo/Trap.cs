using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private Player m_player = null;

    private void Start()
    {
        m_player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == m_player.gameObject)
        {
            Debug.Log("Hit Player");
        }
    }
}
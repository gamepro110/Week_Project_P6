using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMove : MonoBehaviour
{
    private Player m_player;
    private Sprite m_sprite;

    private void Start()
    {
        m_player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        transform.position = m_player.Rig.velocity / 4;
    }
}
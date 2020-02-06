﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MovementMechanics
{
    private Renderer m_SpriteRenderer;
    private GameObject player;
    private Rigidbody2D m_rig;
    private GameManager manager;

    private Vector3 m_startPos;

    private float speed = 0;

    private IEnumerator SpeedAdjustment(float t)
    {
        speed = 10;
        yield return new WaitForSeconds(t);
        speed = 0;
    }

    public void CatchUp(float t)
    {
        StartCoroutine(SpeedAdjustment(t));
        if (!m_SpriteRenderer.isVisible)
        {
            transform.position = new Vector3(Camera.main.transform.position.x + 10, transform.position.y, 0);
        }
    }

    private void Start()
    {
        m_startPos = transform.position;
        EventManager.ResetLevelEvent += ResetLevelEvent;

        m_SpriteRenderer = gameObject.GetComponent<Renderer>();
        player = FindObjectOfType<Player>().gameObject;
        m_rig = gameObject.GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<GameManager>();
        CatchUp(5f);
    }

    private void Update()
    {
        m_rig.velocity = AddForce(new Vector2(-speed, 0), m_rig, 10, 55.1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetInstanceID() == player.GetInstanceID())
        {
            manager.Death();
        }
    }

    private void OnDestroy()
    {
        EventManager.ResetLevelEvent -= ResetLevelEvent;
    }

    private void ResetLevelEvent()
    {
        transform.position = m_startPos;
    }

    public void NerfVelocity()
    {
        m_rig.velocity /= 2;
    }
}
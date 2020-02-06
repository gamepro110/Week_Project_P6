using System.Collections;
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
    private bool Reset = false;
    private bool start = true;

    internal IEnumerator Flicker()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        while (m_SpriteRenderer.isVisible)
        {
            yield return new WaitForSeconds(.2f);
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, .5f);
            yield return new WaitForSeconds(.2f);
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1f);
        }
    }

    IEnumerator SpeedAdjustment(float t)
    {
        speed = 10;
        yield return new WaitForSeconds(t);
        if (Reset) 
        {
            Reset = false;
            StartCoroutine(SpeedAdjustment(t));
            yield break;
        }
        start = true;
        speed = 0;
    }

    /// <summary>
    /// Called when the AI needs to catch up with 't' as duration before decreasing again.
    /// </summary>
    /// <param name="t"></param>
    public void CatchUp(float t)
    {
        if (!start) 
            Reset = true;
        start = false;

        StartCoroutine(SpeedAdjustment(t));
        if (!m_SpriteRenderer.isVisible && t != 5f)
        {
            transform.position = new Vector3(Camera.main.transform.position.x + 10, transform.position.y, 0);
        }
    }
    /// <summary>
    /// Called upon when the AI needs to be stunned (gets a drawback).
    /// </summary>
    public void Stun()
    {
        speed = 0;
        Reset = false;
        start = true;
        StartCoroutine(Flicker());
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
        if (Input.GetKeyDown(KeyCode.X))
        {
            Stun();
        }
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
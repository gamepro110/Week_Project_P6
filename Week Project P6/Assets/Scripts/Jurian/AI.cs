using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MovementMechanics
{
    private Renderer m_SpriteRenderer;
    private GameObject player;
    private Rigidbody2D m_rig;

    private float speed = 0;

    IEnumerator SpeedAdjustment(float t)
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
        m_SpriteRenderer = gameObject.GetComponent<Renderer>();
        player = FindObjectOfType<Player>().gameObject;
        m_rig = gameObject.GetComponent<Rigidbody2D>();
        CatchUp(5f);
    }
    private void Update()
    {
        m_rig.velocity = AddForce(new Vector2(-speed, 0), m_rig, 10, 55.1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerascript : MovementMechanics
{
    [Header("Setup")]
    [SerializeField] private GameObject player = null;
    [SerializeField, Tooltip("Distance that'll be set between the player and the camera")] private Vector3 m_CameraOffset = new Vector3();
    [Header("Follow Axis")]
    [SerializeField] private bool x = true;
    [SerializeField] private bool y = true;

    [SerializeField] private float StaticX = 0f;
    [SerializeField] private float StaticY = 0f;

    private Vector3 LastPos;
    private Rigidbody2D m_Rig;

    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        m_Rig = player.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        LastPos = new Vector3(player.transform.position.x, player.transform.position.y, 12);
        m_CameraOffset = Vector3.Lerp(m_CameraOffset, new Vector3(-5 + (-m_Rig.velocity.x / 5), m_CameraOffset.y, m_CameraOffset.z), 1f * Time.deltaTime);
        Vector3 Pos = UpdateCamera(player.transform.position, m_CameraOffset, LastPos);

        transform.position = new Vector3((x == true) ? Pos.x : StaticX, (y == true) ? Pos.y : StaticY, Pos.z);
    }
}

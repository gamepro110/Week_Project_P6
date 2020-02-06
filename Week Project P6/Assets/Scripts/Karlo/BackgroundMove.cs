using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    private Vector3 m_startpos = Vector3.zero;
    [SerializeField] private float m_speed = 4;

    private void Awake()
    {
        m_startpos = transform.position;
    }

    private void Start()
    {
        EventManager.ResetLevelEvent += ResetLevelEvent;
    }

    private void ResetLevelEvent()
    {
        transform.position = m_startpos;
    }

    private void Update()
    {
        transform.position += new Vector3(-m_speed, 0) * Time.deltaTime;
    }

    private void OnDestroy()
    {
        EventManager.ResetLevelEvent -= ResetLevelEvent;
    }
}
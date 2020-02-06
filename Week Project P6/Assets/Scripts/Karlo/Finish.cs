using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private Player m_player = null;
    private int m_count = 0;

    private void Start()
    {
        m_player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == m_player.gameObject)
        {
            m_count++;

            if (m_count > 2)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPaused = true;
#endif
            }

            EventManager.ResetLevelFunc();
        }
    }
}
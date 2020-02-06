using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    protected Player m_player = null;
    protected AI m_loli = null;

    protected Coroutine m_invincibleRoutine;

    [SerializeField] protected int m_uses = 3;

    private void Start()
    {
        m_player = FindObjectOfType<Player>();
        m_loli = FindObjectOfType<AI>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        m_uses--;

        if (m_uses < 1)
        {
            Destroy(gameObject);
        }
    }

    protected IEnumerator WaitForFlicker()
    {
        while (true)
        {
            if (m_invincibleRoutine == null)
            {
                yield break;
            }
            yield return new WaitForSeconds(0);
        }
    }
}
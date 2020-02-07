using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovementMechanics
{
    #region Variables

    [Header("Movement components")]
    [SerializeField, Range(0, 100), Tooltip("The speed cap in magnitudes (Rig.Velocity).Magnitude (excluding Y)")] private float m_SpeedVelocityCap = 10;

    public float SpeedVelocityCap
    {
        get { return m_SpeedVelocityCap; }
    }

    [SerializeField, Range(1f, 10f)] private float m_maxDashDebounce = 3f;
    [SerializeField, Range(0, 100), Tooltip("The max jump height velocity cap in magnitudes (Rig.Velocity).Magnitude (excluding X and Z)")] private float m_JumpVelocityCap = 10;

    [SerializeField] private Trap m_heldTrap = null;
    [SerializeField] private Vector2 m_WalkSpeed = new Vector2();
    [SerializeField] private Vector2 m_JumpPower = new Vector2();

    private Transform m_CameraTransform;
    private Rigidbody2D m_Rig;

    private Vector2 m_oldWalkSpeed;

    private Vector2 m_CurrentJump;
    private Vector3 LastPos;

    private Collider2D m_PlayerCollider;
    private float m_time;

    private bool Debounce;

    private bool invincible;

    /// <summary>
    /// Set the invincible state to true or false
    /// </summary>
    public bool Invincible
    {
        get { return invincible; }
        set { invincible = value; }
    }

    public Rigidbody2D Rig => m_Rig;

    #region temp reset

    [SerializeField] private Transform m_startPos;

    #endregion temp reset

    #endregion Variables

    #region Custom Functions

    internal IEnumerator Flicker()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        while (invincible)
        {
            yield return new WaitForSeconds(.2f);
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, .5f);
            yield return new WaitForSeconds(.2f);
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1f);
        }
    }

    private IEnumerator Dash()
    {
        m_time = 0f;
        Vector2 _OldVelocity = m_WalkSpeed;
        m_WalkSpeed = new Vector2(m_WalkSpeed.x * 3, 0);
        yield return new WaitForSeconds(1f);
        m_WalkSpeed = _OldVelocity;
    }

    private IEnumerator SetInvincible()
    {
        m_oldWalkSpeed = m_WalkSpeed;
        NerfVelocity();
        m_WalkSpeed = new Vector2();
        StartCoroutine(Flicker());
        yield return new WaitForSeconds(.2f);
        m_WalkSpeed = m_oldWalkSpeed;
        yield return new WaitForSeconds(3f);
        invincible = false;
        Debounce = false;
    }

    internal void NerfVelocity()
    {
        m_Rig.velocity /= 2;
    }

    internal IEnumerator SlowEffect(float effectTime)
    {
        m_WalkSpeed /= 2;
        yield return new WaitForSeconds(effectTime);
        m_WalkSpeed = m_oldWalkSpeed;
    }

    #endregion Custom Functions

    #region Unity built-in Functions

    private void Start()
    {
        m_Rig = gameObject.GetComponent<Rigidbody2D>();
        m_PlayerCollider = GetComponent<BoxCollider2D>();
        m_oldWalkSpeed = m_WalkSpeed;

        m_startPos.position = transform.position;
        EventManager.ResetLevelEvent += ResetLevelEvent;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EventManager.ResetLevelFunc();
        }
#endif

        m_time += Time.deltaTime;
        SpawnTrapCheck();
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, -transform.up, m_PlayerCollider.bounds.extents.y + .1f, LayerMask.GetMask("StaticFloor") + LayerMask.GetMask("Floor"));

        LastPos = transform.position;

        if (Input.GetKeyDown(KeyCode.S)) // Dashdown
        {
            if (!raycastHit.collider)
            {
                m_WalkSpeed = new Vector2(m_WalkSpeed.x, -100);
            }
        }

        if (invincible && !Debounce)
        {
            Debounce = true;
            StartCoroutine(SetInvincible());
        }

        if (raycastHit.collider)
        {
            if (Input.GetKeyDown(KeyCode.D)) // Roll
            {
                if (m_time >= m_maxDashDebounce)
                {
                    StartCoroutine(Dash());
                }
            }

            if (Input.GetKeyDown(KeyCode.Space)) // jump
            {
                m_CurrentJump = m_JumpPower;
            }

            m_WalkSpeed = new Vector2(m_WalkSpeed.x, 0);
        }

        m_Rig.velocity = AddForce(m_WalkSpeed + m_CurrentJump, m_Rig, m_SpeedVelocityCap, m_JumpVelocityCap);
        m_CurrentJump = new Vector2();

        if (!invincible)
        {
            m_Rig.velocity = new Vector2(-m_SpeedVelocityCap, m_Rig.velocity.y);
        }
    }

    private void OnDestroy()
    {
        EventManager.ResetLevelEvent -= ResetLevelEvent;
    }

    #endregion Unity built-in Functions

    public void PickupTrap(Trap _trap)
    {
        if (m_heldTrap == null)
        {
            m_heldTrap = _trap;
        }
    }

    public void SpawnTrapCheck()
    {
        if (m_heldTrap != null)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, -transform.up, m_PlayerCollider.bounds.extents.y + .1f);
                if (raycastHit.collider)
                {
                    Vector3 spawnpos = transform.position;
                    spawnpos.x -= -1.5f;
                    Instantiate(m_heldTrap, spawnpos, Quaternion.identity);
                    m_heldTrap = null;
                }
            }
        }
    }

    private void ResetLevelEvent() => transform.position = m_startPos.position;
}
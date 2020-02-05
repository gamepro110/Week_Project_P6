using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovementMechanics
{
    #region Variables

    [Header("Setup")]
    [SerializeField] private Camera Camera = null;

    [SerializeField, Tooltip("Distance that'll be set between the player and the camera")] private Vector3 m_CameraOffset = new Vector3();

    [Header("Movement components")]
    [SerializeField, Range(10, 100), Tooltip("The speed cap in magnitudes (Rig.Velocity).Magnitude (excluding Y)")] private float m_SpeedVelocityCap = 10;

    [SerializeField, Range(10, 100), Tooltip("The max jump height velocity cap in magnitudes (Rig.Velocity).Magnitude (excluding X and Z)")] private float m_JumpVelocityCap = 10;
    [SerializeField] private Trap m_heldTrap = null;

    [SerializeField] private Vector2 m_WalkSpeed = new Vector2();
    [SerializeField] private Vector2 m_JumpPower = new Vector2();

    private Transform m_CameraTransform;
    private Rigidbody2D m_Rig;

    private Vector2 m_CurrentJump;
    private Vector3 LastPos;

    private Collider2D m_PlayerCollider;

    private bool invincible;
    /// <summary>
    /// Set the invincible state to true or false
    /// </summary>
    public bool Invincible 
    { 
        get { return invincible; } 
        set { invincible = value; } 
    }

    #endregion
    #region Custom Functions
    
    private IEnumerator SetInvincible()
    {
        invincible = false;
        gameObject.layer = LayerMask.GetMask("Invincible State");
        yield return new WaitForSeconds(2f);
        gameObject.layer = LayerMask.GetMask("Player");
    }
    #endregion
    #region Unity built-in Functions

    private void Start()
    {
        m_CameraTransform = Camera.transform;
        m_Rig = gameObject.GetComponent<Rigidbody2D>();
        m_PlayerCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        SpawnTrapCheck();
        
        LastPos = transform.position;

        if (Input.GetKeyDown(KeyCode.D))
        {
            //TODO roll
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, -transform.up, m_PlayerCollider.bounds.extents.y+.1f, LayerMask.GetMask("Floor"));
            if (raycastHit.collider)
            {
                m_CurrentJump = m_JumpPower;
            }
        }
        if (invincible)
        {
            StartCoroutine(SetInvincible());
            //TODO let the little girl catch up
        }
    }

    private void LateUpdate()
    {
        m_CameraOffset = Vector3.Lerp(m_CameraOffset, new Vector3(-3+(-m_Rig.velocity.x/5), m_CameraOffset.y, m_CameraOffset.z), 1f * Time.deltaTime);
        Vector3 Pos = UpdateCamera(gameObject.transform.position, m_CameraOffset, LastPos);

        Camera.transform.position = Pos;

        m_Rig.velocity = AddForce(m_WalkSpeed + m_CurrentJump, m_Rig, m_SpeedVelocityCap, m_JumpVelocityCap);
        m_CurrentJump = new Vector2();
    }

    #endregion Unity built-in Functions

    public void PickupTrap(Trap _trap)
    {
        m_heldTrap = _trap;
    }

    public void SpawnTrapCheck()
    {
        if (m_heldTrap != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Instantiate(m_heldTrap, transform.position, Quaternion.identity);
                m_heldTrap = null;
            }
        }
    }
}
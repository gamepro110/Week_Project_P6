using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables

    [Header("Setup")]
    [SerializeField] private Camera Camera = null;

    [SerializeField, Tooltip("Distance that'll be set between the player and the camera")] private Vector3 m_CameraOffset = new Vector3();

    [Header("Movement components")]
    [SerializeField, Range(10, 100), Tooltip("The speed cap in magnitudes (Rig.Velocity).Magnitude (excluding Y)")] private float m_SpeedVelocityCap = 10;

    [SerializeField, Range(10, 100), Tooltip("The max jump height velocity cap in magnitudes (Rig.Velocity).Magnitude (excluding X and Z)")] private float m_JumpVelocityCap = 10;
    [SerializeField] private Trap m_heldTrap = null;

    [SerializeField] private Vector3 WalkSpeed = Vector3.zero;
    [SerializeField] private Vector3 JumpPower = Vector3.zero;

    private Transform m_CameraTransform;
    private Rigidbody m_Rig;

    #endregion Variables

    #region Custom Functions

    /// <summary>
    /// Updates the camera's position and rotation smoothly to the given Position and eulerAngle parameter.
    /// </summary>
    /// <param name="_Pos"></param>
    /// <param name="_Euler"></param>
    private void UpdateCamera(Vector3 _Pos)
    {
        m_CameraTransform.position = _Pos + m_CameraOffset;
        m_CameraTransform.LookAt(gameObject.transform);
    }

    /// <summary>
    /// Adds force to the player dependant on the cap of the walk and jump velocity
    /// </summary>
    /// <param name="_Velocity"></param>
    private void AddForce(Vector3 _Velocity)
    {
        m_Rig.AddForce(_Velocity);

        Vector3 _CappedXZ = Vector3.ClampMagnitude(new Vector3(m_Rig.velocity.x, 0, m_Rig.velocity.z), m_SpeedVelocityCap);
        float _CappedY = Vector3.ClampMagnitude(new Vector3(0, m_Rig.velocity.y, 0), m_JumpVelocityCap).y;

        m_Rig.velocity = new Vector3(_CappedXZ.x, _CappedY, _CappedXZ.z);
    }

    #endregion Custom Functions

    #region Unity built-in Functions

    private void Start()
    {
        m_CameraTransform = Camera.transform;
        m_Rig = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        SpawnTrapCheck();

        Vector3 _tempVelocity = new Vector3();
        if (Input.GetKey(KeyCode.A))
        {
            _tempVelocity = WalkSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _tempVelocity = -WalkSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _tempVelocity += JumpPower;
        }
        AddForce(_tempVelocity);
    }

    private void LateUpdate()
    {
        UpdateCamera(gameObject.transform.position);
        //AddForce(WalkSpeed + JumpPower);
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
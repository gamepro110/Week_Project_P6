using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject m_camera;
    [SerializeField, Range(5, 100)] private int m_CameraDistance;
    [SerializeField] private Trap m_heldTrap = null;

    private Transform CameraTransform;

    private void UpdateCamera()
    {
        CameraTransform.position = Vector3.Lerp(CameraTransform.position, gameObject.transform.position, .1f);
    }

    private void Start()
    {
        CameraTransform = m_camera.transform;
    }

    private void Update()
    {
        UpdateCamera();
    }

    public void PickupTrap(Trap _trap)
    {
        m_heldTrap = _trap;
    }
}
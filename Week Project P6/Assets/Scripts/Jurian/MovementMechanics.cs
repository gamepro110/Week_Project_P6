using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMechanics : MonoBehaviour
{
    /// <summary>
    /// Adds force to the player dependant on the cap of the walk and jump velocity.
    /// </summary>
    /// <param name="_Velocity"></param>
    /// <param name="m_Rig"></param>
    /// <param name="m_SpeedVelocityCap"></param>
    /// <param name="m_JumpVelocityCap"></param>
    /// <returns>Vector3</returns>
    public Vector3 AddForce(Vector2 _Velocity, Rigidbody2D _Rig, float _SpeedVelocityCap, float _JumpVelocityCap)
    {
        _Rig.AddForce(_Velocity);

        float _CappedX = Vector2.ClampMagnitude(new Vector3(_Rig.velocity.x, 0, 0), _SpeedVelocityCap).x;
        float _CappedY = Vector2.ClampMagnitude(new Vector3(0, _Rig.velocity.y, 0), _JumpVelocityCap).y;

        return new Vector3(_CappedX, _CappedY);
    }

    /// <summary>
    /// Calculates and returns the required camera movement.
    /// </summary>
    /// <param name="EndPosition"></param>
    /// <param name="CameraOffset"></param>
    /// <param name="LastPosition"></param>
    /// <param name="CameraTransform"></param>
    /// <returns>Struct</returns>
    public Vector3 UpdateCamera(Vector3 EndPosition, Vector3 CameraOffset, Vector3 LastPosition)
    {
        return Vector3.Lerp(LastPosition + CameraOffset, EndPosition + CameraOffset, .1f);
        
    }
}

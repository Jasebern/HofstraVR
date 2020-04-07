using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;

    #region Private Variables
    private PlayerMotor motor; 

    private float _xMov; // Player Movement X Plane
    private float _zMov; // Player Movement Z Plane
    private float _xRot; // VERTIVAL Camera Rotation 
    private float _yRot; // HORIZONTAL Player Rotation

    private Vector3 _movHorizontal;
    private Vector3 _movVertical;
    private Vector3 _velocity;
    private Vector3 _rotation;
    private Vector3 _cameraRotation;
    #endregion

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        // Calculate movement velocity as a 3D Vector
        _xMov = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        _zMov = CrossPlatformInputManager.GetAxisRaw("Vertical");
        _movHorizontal = transform.right * _xMov; 
        _movVertical = transform.forward * _zMov;

        // Final Movement Vector
        _velocity = (_movHorizontal + _movVertical).normalized * speed;

        // Apply Movement
        motor.Move(_velocity);


        // Calculate Rotation as a 3D Vector (Turning LEFT and RIGHT)
        _yRot = CrossPlatformInputManager.GetAxisRaw("Mouse X");
        _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;


        // Apply Rotation
        motor.Rotate(_rotation);


        // Calculate CAMERA Rotation as a 3D Vector (Turning UP and DOWN)
        _xRot = CrossPlatformInputManager.GetAxisRaw("Mouse Y");
        _cameraRotation = new Vector3(_xRot, 0f, 0f) * lookSensitivity;


        // Apply Rotation
        motor.RotateCamera(_cameraRotation);

    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    protected Camera cam;

    protected Vector3 velocity = Vector3.zero;
    protected Vector3 rotation = Vector3.zero;
    protected Vector3 cameraRotation = Vector3.zero;

    protected Rigidbody rb;
    protected Animator myAnimator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //myAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// Updates current velocity by taking the input: _velocity
    /// </summary>
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    /// <summary>
    /// Updates current horizontal rotational vector of the PLAYER by taking the input: _rotation
    /// </summary>
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    /// <summary>
    /// Updates the current vertical rotational vector of the CAMERA by taking the input: _cameraRotation
    /// </summary>
    public void RotateCamera(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }

    /// <summary>
    /// Run every physics iteration.
    /// </summary>
    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    /// <summary>
    /// Perform movement based on the velocity variable.
    /// </summary>
    protected virtual void PerformMovement()
    {
        if(velocity != Vector3.zero)
        {
            // Move rigidbody to the player position + the player's velocity
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    /// <summary>
    /// Perform horizontal rotation based on the rotation variable.
    /// </summary>
    protected virtual void PerformRotation()
    {
        // Rotate rigidbody based on the current rotation
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            cam.transform.Rotate(-cameraRotation);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatObject : MonoBehaviour
{
    public float waterLevel = 0f;
    public float floatThreshold = 2f;
    public float waterDensity = 0.125f;
    public float downForce = 4f;

    private float forceFactor;
    private Vector3 floatForce;
    private Rigidbody myRigidbody;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        forceFactor = 1f - ((transform.position.y - waterLevel) / floatThreshold);

        if(forceFactor > 0f)
        {
            floatForce = -Physics.gravity * myRigidbody.mass * (forceFactor - myRigidbody.velocity.y * waterDensity);
            floatForce += new Vector3(0f, -downForce * myRigidbody.mass, 0f);
            myRigidbody.AddForceAtPosition(floatForce, transform.position);
        }
    }
}

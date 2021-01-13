using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody myRigidbody;

    void Start()
    {
        myRigidbody = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProcessInput();
    }

    void ProcessInput()
    {
        // Thrust 'Space'
        if (Input.GetKey(KeyCode.Space))
        {
            myRigidbody.AddRelativeForce(Vector3.up);
        }

        // Rotate 'wAsD'
        if (Input.GetKey(KeyCode.A))
        {

        }
        else if (Input.GetKey(KeyCode.D))
        {

        }
    }
}

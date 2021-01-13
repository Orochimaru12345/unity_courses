using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rotatingForce = 1f;

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
            this.transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * rotatingForce);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(-Vector3.forward * Time.deltaTime * rotatingForce);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingAsteroid : MonoBehaviour
{
    [SerializeField] float minVelocity = 9.5f;
    [SerializeField] float maxVelocity = 10.0f;

    float myVelocity;
    float myDirection;
    Rigidbody myRigidBody;

    private void Start()
    {
        myVelocity = Random.Range(minVelocity, maxVelocity);

        myDirection = (Random.Range(0, 10) > 5) ? 1f : -1f;

        myRigidBody = GetComponent<Rigidbody>();

        // myRigidBody.velocity = Vector3.up * myDirection * myVelocity;
    }

    private void Update()
    {
        myRigidBody.velocity = Vector3.up * myDirection * myVelocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (transform.position.y > collision.gameObject.transform.position.y)
        {
            myDirection = 1f;
        }
        else
        {
            myDirection = -1f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float movementSpeed = 0.3f;
    private Rigidbody myRigidBody = null;
    private bool isMovable = false;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (isMovable)
        {
            float xVelocity = Input.GetAxis("Horizontal") * movementSpeed;
            float yVelocity = Input.GetAxis("Vertical") * movementSpeed;

            myRigidBody.AddForce(new Vector3(xVelocity, 0, yVelocity), ForceMode.Force);
        }
    }

    public void SetIsMovable(bool value)
    {
        isMovable = value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rotatingForce = 1f;
    [SerializeField] float thrustingForce = 1f;
    [SerializeField] float thrustingVolume = 0.8f;
    [SerializeField] float thrustingPitch = 1.6f;

    Rigidbody myRigidbody;
    AudioSource myAudioSource;
    float defaultAudioSourcePitch = 0f;
    float defaultAudioSourceVolume = 0f;

    const string FRIENDLY = "Friendly";

    private void Start()
    {
        myRigidbody = this.GetComponent<Rigidbody>();
        myAudioSource = this.GetComponent<AudioSource>();
        defaultAudioSourcePitch = myAudioSource.pitch;
        defaultAudioSourceVolume = myAudioSource.volume;
    }

    private void Update()
    {
        ProcessInput();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case FRIENDLY:
                print("friendly");
                break;
            default:
                print("FOK!");
                break;
        }
    }

    void ProcessInput()
    {
        Thrusting();
        Rotating();
    }

    private void Rotating()
    {
        myRigidbody.freezeRotation = true;
        // Rotate 'wAsD'
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * rotatingForce);
            myRigidbody.ResetInertiaTensor();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(-Vector3.forward * Time.deltaTime * rotatingForce);
            myRigidbody.ResetInertiaTensor();
        }
        myRigidbody.freezeRotation = false;
    }

    private void Thrusting()
    {
        // Thrust 'Space'
        if (Input.GetKey(KeyCode.Space))
        {
            myRigidbody.AddRelativeForce(Vector3.up * thrustingForce);
            SetThrustingSound();
        }
        else
        {
            SetIdleSound();
        }
    }

    void SetThrustingSound()
    {
        if (myAudioSource)
        {
            myAudioSource.pitch = thrustingPitch;
            myAudioSource.volume = thrustingVolume;
        }
    }

    void SetIdleSound()
    {
        if (myAudioSource)
        {
            myAudioSource.pitch = defaultAudioSourcePitch;
            myAudioSource.volume = defaultAudioSourceVolume;
        }
    }
}

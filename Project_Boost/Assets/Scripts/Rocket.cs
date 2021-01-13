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

    void Start()
    {
        myRigidbody = this.GetComponent<Rigidbody>();
        myAudioSource = this.GetComponent<AudioSource>();
        defaultAudioSourcePitch = myAudioSource.pitch;
        defaultAudioSourceVolume = myAudioSource.volume;
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
            myRigidbody.AddRelativeForce(Vector3.up * thrustingForce);
            SetThrustingSound();
        }
        else
        {
            SetIdleSound();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Tooltip("Acceleration")] [SerializeField] float thrustingForce = 500f;
    [Tooltip("Thrusting sound volume")] [SerializeField] float thrustingVolume = 0.8f;
    [Tooltip("Thrusting sound pitch")] [SerializeField] float thrustingPitch = 1.6f;
    float defaultEnginePitch = 0f;
    float defaultEngineVolume = 0f;

    [Tooltip("Rotation")] [SerializeField] float rotatingForce = 50f;

    Rigidbody myRigidbody = null;
    AudioSource myEngineAudioSource = null;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myEngineAudioSource = GetEngineAudioSource();

        // use them while rocket is not thrusting
        GetDefaultEngineSoundProperties();
        // run and loop engine sound
        StartEngineSound();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    // Fly with spacebar.
    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyAcceleration();
            SetThrustingEngineSound();
        }
        else
        {
            SetIdleEngineSound();
        }
    }

    // Rotate with wAsD.
    private void ProcessRotation()
    {
        // LEFT.
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotatingForce);
        }
        // RIGHT.
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotatingForce);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        RemovePhysicsRotation();
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
    }

    private void RemovePhysicsRotation()
    {
        myRigidbody.angularVelocity = Vector3.zero;
    }

    private void ApplyAcceleration()
    {
        myRigidbody.AddRelativeForce(Vector3.up * thrustingForce * Time.deltaTime, ForceMode.Acceleration);
    }

    private AudioSource GetEngineAudioSource()
    {
        foreach (var audioSource in GetComponents<AudioSource>())
        {
            if (audioSource.clip.name.Contains("thrust"))
            {
                return audioSource;
            }
        }
        Debug.LogError("MY_ERROR: AudioSource '*thrust*' not found.");
        return null;
    }

    private void GetDefaultEngineSoundProperties()
    {
        if (myEngineAudioSource)
        {
            defaultEnginePitch = myEngineAudioSource.pitch;
            defaultEngineVolume = myEngineAudioSource.volume;
        }
    }

    private void SetThrustingEngineSound()
    {
        myEngineAudioSource.pitch = thrustingPitch;
        myEngineAudioSource.volume = thrustingVolume;
    }

    private void SetIdleEngineSound()
    {
        myEngineAudioSource.pitch = defaultEnginePitch;
        myEngineAudioSource.volume = defaultEngineVolume;
    }

    private void StartEngineSound()
    {
        if (!myEngineAudioSource.isPlaying)
        {
            myEngineAudioSource.Play();
        }
        if (!myEngineAudioSource.loop)
        {
            myEngineAudioSource.loop = true;
        }
    }
}

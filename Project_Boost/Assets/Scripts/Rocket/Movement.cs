using UnityEngine;

public class Movement : MonoBehaviour
{
    // parameters
    [Tooltip("Acceleration")] [SerializeField] float thrustingForce = 500f;
    [Tooltip("Engine sound")] [SerializeField] AudioClip engineAudioClip = null;
    [Tooltip("Thrusting sound volume")] [SerializeField] float thrustingVolume = 0.8f;
    [Tooltip("Thrusting sound pitch")] [SerializeField] float thrustingPitch = 1.6f;
    
    [Tooltip("Rotation")] [SerializeField] float rotatingForce = 50f;

    // cache
    Rigidbody myRigidbody = null;
    AudioSource engineAudioSource = null;

    // state
    bool isEnabled = true;
    float defaultEnginePitch = 0f;
    float defaultEngineVolume = 0f;

    public void StopEngineSound()
    {
        if (engineAudioSource.isPlaying)
        {
            engineAudioSource.Stop();
        }
    }

    public void StartEngineSound()
    {
        if (!engineAudioSource.isPlaying)
        {
            engineAudioSource.Play();
        }
        if (!engineAudioSource.loop)
        {
            engineAudioSource.loop = true;
        }
    }

    public void StopMovement()
    {
        isEnabled = false;
    }

    public void StartMovement()
    {
        isEnabled = true;
    }

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        engineAudioSource = GetEngineAudioSource();

        // use them while rocket is not thrusting
        GetDefaultEngineSoundProperties();
        // run and loop engine sound
        StartEngineSound();
        // allow to move
        StartMovement();
    }

    void Update()
    {
        if (isEnabled)
        {
            ProcessThrust();
            ProcessRotation();
        }
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
        if (engineAudioSource)
        {
            defaultEnginePitch = engineAudioSource.pitch;
            defaultEngineVolume = engineAudioSource.volume;
        }
    }

    private void SetThrustingEngineSound()
    {
        engineAudioSource.pitch = thrustingPitch;
        engineAudioSource.volume = thrustingVolume;
    }

    private void SetIdleEngineSound()
    {
        engineAudioSource.pitch = defaultEnginePitch;
        engineAudioSource.volume = defaultEngineVolume;
    }
}

using UnityEngine;

public class Movement : MonoBehaviour
{
    // parameters
    [Tooltip("Acceleration")] [SerializeField] float thrustingForce = 500f;
    [Tooltip("Thrusting sound volume")] [SerializeField] float thrustingVolume = 0.8f;
    [Tooltip("Thrusting sound pitch")] [SerializeField] float thrustingPitch = 1.6f;

    [Tooltip("Rotation")] [SerializeField] float rotatingForce = 50f;

    [Tooltip("Engine Particles")] [SerializeField] ParticleSystem engineParticles = null;
    [Tooltip("Thrusting particles speed")] [SerializeField] float thrustingParticlesStartSpeed = 15f;
    [Tooltip("Thrusting particles rate Over time")] [SerializeField] float thrustingParticlesRateOverTime = 150f;

    // cache
    Rigidbody myRigidbody = null;
    AudioSource engineAudioSource = null;

    // state
    bool isEnabled = true;
    float defaultEnginePitch = 0f;
    float defaultEngineVolume = 0f;
    ParticleSystem.MinMaxCurve defaultEngineParticlesStartSpeed = 0f;
    ParticleSystem.MinMaxCurve defaultEngineParticlesRateOverTime = 0f;

    public void StopEngine()
    {
        // play sound
        if (engineAudioSource.isPlaying)
        {
            engineAudioSource.Stop();
        }
        // play particles
        if (engineParticles.isPlaying)
        {
            engineParticles.Stop();
        }
    }

    public void StartEngine()
    {
        // pause sound
        if (!engineAudioSource.isPlaying)
        {
            engineAudioSource.Play();
        }
        if (!engineAudioSource.loop)
        {
            engineAudioSource.loop = true;
        }
        // pause particles
        if (!engineParticles.isPlaying)
        {
            engineParticles.Play();
        }
        if (!engineParticles.main.loop)
        {
            var m = engineParticles.main;
            m.loop = true;
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
        GetDefaultEngineSoundSettings();
        GetDefaultEngineParticlesSettings();
        // run and loop engine sound
        StartEngine();
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
            SetThrustingEngineParticles();
            var a = engineParticles.main;
            a.startSpeed = 15f;
        }
        else
        {
            SetIdleEngineSound();
            SetIdleEngineParticles();
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

    private void GetDefaultEngineSoundSettings()
    {
        if (engineAudioSource)
        {
            defaultEnginePitch = engineAudioSource.pitch;
            defaultEngineVolume = engineAudioSource.volume;
        }
    }

    private void GetDefaultEngineParticlesSettings()
    {
        if (engineParticles)
        {
            var m = engineParticles.main;
            defaultEngineParticlesStartSpeed = m.startSpeed;

            var e = engineParticles.emission;
            defaultEngineParticlesRateOverTime = e.rateOverTime;
        }
    }

    private void SetThrustingEngineSound()
    {
        engineAudioSource.pitch = thrustingPitch;
        engineAudioSource.volume = thrustingVolume;
    }

    private void SetThrustingEngineParticles()
    {
        var m = engineParticles.main;
        m.startSpeed = new ParticleSystem.MinMaxCurve(thrustingParticlesStartSpeed);

        var e = engineParticles.emission;
        e.rateOverTime = new ParticleSystem.MinMaxCurve(thrustingParticlesRateOverTime);
    }

    private void SetIdleEngineSound()
    {
        engineAudioSource.pitch = defaultEnginePitch;
        engineAudioSource.volume = defaultEngineVolume;
    }

    private void SetIdleEngineParticles()
    {
        var m = engineParticles.main;
        m.startSpeed = defaultEngineParticlesStartSpeed;

        var e = engineParticles.emission;
        e.rateOverTime = defaultEngineParticlesRateOverTime;
    }
}

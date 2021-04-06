using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float thrustingForce = 14f; // fix bug, when rocket does not thrust
    [SerializeField] float thrustingVolume = 0.8f;
    [SerializeField] float thrustingPitch = 1.6f;
    [SerializeField] ParticleSystem mainEngineParticles = null;
    [SerializeField] ParticleSystem landingParticles = null;
    [SerializeField] ParticleSystem explosionParticles = null;

    AudioSource engineAudioSource;
    AudioSource explosionAudioSource;
    float defaultAudioSourcePitch = 0f;
    float defaultAudioSourceVolume = 0f;
    bool collisionsDisabled = false;
    ParticleSystem.MainModule engineMainModule;
    ParticleSystem.EmissionModule engineEmissionModule;

    enum State
    {
        Alive,
        Dying,
        Transcending
    };
    State state = State.Alive;

    const string FRIENDLYTAG = "Friendly";
    const string FINISH_TAG = "Finish";

    private void Start()
    {
        foreach (var audioSource in GetComponents<AudioSource>())
        {
            if (audioSource.clip.name == "explosion")
            {
                explosionAudioSource = audioSource;
            }
            else
            {
                engineAudioSource = audioSource;
                defaultAudioSourcePitch = engineAudioSource.pitch;
                defaultAudioSourceVolume = engineAudioSource.volume;
            }
        }

        engineMainModule = mainEngineParticles.main;
        engineEmissionModule = mainEngineParticles.emission;
    }

    private void Update()
    {
        ProcessInput();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || collisionsDisabled)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case FRIENDLYTAG:
                print("friendly");
                break;
            case FINISH_TAG:
                LandSuccessfully();
                break;
            default:
                Explode(collision);
                break;
        }
    }

    void ProcessInput()
    {
        RespondToThrustInput();
        RespondToDebugKeys();
        RespondToEsc();
    }

    private void RespondToThrustInput()
    {
        // Thrust 'Space'
        if (Input.GetKey(KeyCode.Space) && state == State.Alive)
        {
            //myRigidbody.AddRelativeForce(Vector3.up * thrustingForce * Time.deltaTime, ForceMode.Acceleration);
            SetThrustingSound();
            SetThrustingParticles();
        }
        else
        {
            SetIdleSound();
            SetIdleParticles();
        }
    }

    void SetThrustingParticles()
    {
        engineMainModule.startSpeed = 15f;
        engineEmissionModule.rateOverTime = 100;
    }

    void SetThrustingSound()
    {
        if (engineAudioSource)
        {
            engineAudioSource.pitch = thrustingPitch;
            engineAudioSource.volume = thrustingVolume;
        }
    }

    void SetIdleParticles()
    {
        engineMainModule.startSpeed = 5f;
        engineEmissionModule.rateOverTime = 50;
    }

    void SetIdleSound()
    {
        if (engineAudioSource)
        {
            engineAudioSource.pitch = defaultAudioSourcePitch;
            engineAudioSource.volume = defaultAudioSourceVolume;
        }
    }

    void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentScene + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    void Explode(Collision collision)
    {
        Debug.Log("I hit: '" + collision.gameObject.name + "'");

        engineAudioSource.Stop();
        explosionAudioSource.Play();
        explosionParticles.Play();
        state = State.Dying;

        Invoke("LoadStartScene", 1);
    }

    void LandSuccessfully()
    {
        state = State.Transcending;
        landingParticles.Play();
        Invoke("LoadNextScene", 1);
    }

    void RespondToDebugKeys()
    {
        if (Debug.isDebugBuild)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                LoadNextScene();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                collisionsDisabled = !collisionsDisabled;
            }
        }
    }

    void RespondToEsc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(0);
        }
    }
}

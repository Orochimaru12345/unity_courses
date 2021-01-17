using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rotatingForce = 1f;
    [SerializeField] float thrustingForce = 1f;
    [SerializeField] float thrustingVolume = 0.8f;
    [SerializeField] float thrustingPitch = 1.6f;
    [SerializeField] ParticleSystem mainEngineParticles = null;
    [SerializeField] ParticleSystem landingParticles = null;
    [SerializeField] ParticleSystem explosionParticles = null;

    Rigidbody myRigidbody;
    AudioSource engineAudioSource;
    AudioSource explosionAudioSource;
    float defaultAudioSourcePitch = 0f;
    float defaultAudioSourceVolume = 0f;

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
        myRigidbody = this.GetComponent<Rigidbody>();

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
    }

    private void Update()
    {
        ProcessInput();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
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
        RespondToRotateInput();
    }

    private void RespondToRotateInput()
    {
        myRigidbody.freezeRotation = true;
        // Rotate 'wAsD'
        if (Input.GetKey(KeyCode.A) && state == State.Alive)
        {
            this.transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * rotatingForce);
            myRigidbody.ResetInertiaTensor();
        }
        else if (Input.GetKey(KeyCode.D) && state == State.Alive)
        {
            this.transform.Rotate(-Vector3.forward * Time.deltaTime * rotatingForce);
            myRigidbody.ResetInertiaTensor();
        }
        myRigidbody.freezeRotation = false;
    }

    private void RespondToThrustInput()
    {
        // Thrust 'Space'
        if (Input.GetKey(KeyCode.Space) && state == State.Alive)
        {
            myRigidbody.AddRelativeForce(Vector3.up * thrustingForce);
            SetThrustingSound();
            // mainEngineParticles.main.startLifetime = 1f;
            var main = mainEngineParticles.main;
            main.startSpeed = 15f;
            var emission = mainEngineParticles.emission;
            emission.rateOverTime = 100;
        }
        else
        {
            SetIdleSound();
            var main = mainEngineParticles.main;
            main.startSpeed = 5f;
            var emission = mainEngineParticles.emission;
            emission.rateOverTime = 50;
        }
    }

    void SetThrustingSound()
    {
        if (engineAudioSource)
        {
            engineAudioSource.pitch = thrustingPitch;
            engineAudioSource.volume = thrustingVolume;
        }
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
        if (currentScene < SceneManager.sceneCountInBuildSettings)
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
}

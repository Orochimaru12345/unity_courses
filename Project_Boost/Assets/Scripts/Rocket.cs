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

    Rigidbody myRigidbody;
    AudioSource myAudioSource;
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
                state = State.Transcending;
                Invoke("LoadNextScene", 1);
                break;
            default:
                print("shit = " + collision.gameObject.name);
                state = State.Dying;
                Invoke("LoadStartScene", 1);
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

    private void Thrusting()
    {
        // Thrust 'Space'
        if (Input.GetKey(KeyCode.Space) && state == State.Alive)
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
}

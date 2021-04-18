using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float onHitLevelLoadDelay = 1.2f;
    [SerializeField] AudioClip finish = null;
    [SerializeField] AudioClip crash = null;
    [SerializeField] ParticleSystem finishParticles = null;
    [SerializeField] ParticleSystem crashParticles = null;

    Movement myMovement = null;
    AudioSource freeAudioSource = null; // any audio source without a clip
    bool isTransitioning = false;

    private void Start()
    {
        GetFreeAudioSource();

        myMovement = FindObjectOfType<Movement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit game object with tag: '" + collision.gameObject.tag + "'");

        switch (collision.gameObject.tag)
        {
            case PBConsts.FRIENDLY_TAG:
                // rocket can touch a launch pad without any consequences
                break;
            case PBConsts.FINISH_TAG:
                ProcessLandingPadTouch();
                break;
            default:
                ProcessObstacleCrash();
                break;
        }
    }

    private void ProcessLandingPadTouch()
    {
        if (!isTransitioning)
        {
            isTransitioning = true;
            freeAudioSource.PlayOneShot(finish);
            PlayParticlesAtRocket(finishParticles);
            TurnOffRocket();
            Invoke("LoadNextLevel", onHitLevelLoadDelay);
        }
    }

    private void ProcessObstacleCrash()
    {
        if (!isTransitioning)
        {
            isTransitioning = true;
            freeAudioSource.PlayOneShot(crash);
            PlayParticlesAtRocket(crashParticles);
            TurnOffRocket();
            Invoke("LoadStartMenu", onHitLevelLoadDelay);
        }
    }

    private void LoadStartMenu()
    {
        SceneManager.LoadScene(PBConsts.MENU_SCENE);
    }

    private void LoadNextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = PBConsts.MENU_SCENE;
        }
        SceneManager.LoadScene(nextScene);
    }

    private void TurnOffRocket()
    {
        if (myMovement)
        {
            myMovement.StopEngine();
            myMovement.StopMovement();
        }
    }

    private void GetFreeAudioSource()
    {
        foreach (var a in GetComponents<AudioSource>())
        {
            if (!a.clip)
            {
                freeAudioSource = a;
            }
        }
    }

    private void PlayParticlesAtRocket(ParticleSystem p)
    {
        ParticleSystem ps = Instantiate<ParticleSystem>(p, transform);
        ps.Play();
    }
}

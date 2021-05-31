using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem explosionVfx;

    void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        GetComponent<PlayerControls>().enabled = false;

        explosionVfx.Play();

        GetComponent<BoxCollider>().enabled = false;

        Invoke(nameof(ReloadLevel), loadDelay);
    }

    void ReloadLevel()
    {
        var sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }
}

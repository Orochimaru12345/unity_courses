using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    Mover player;
    WinText winText;

    private void Start()
    {
        player = FindObjectOfType<Mover>();
        winText = FindObjectOfType<WinText>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("WIN");
            StartCoroutine(FinishScene(player));
        }
    }

    IEnumerator FinishScene(Mover player)
    {
        if (player)
        {
            player.SetIsMovable(false);
        }
        else
        {
            Debug.LogWarning("Player not found!");
        }

        if (winText)
        {
            winText.ShowWinText();
        }

        yield return new WaitForSeconds(2);

        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}

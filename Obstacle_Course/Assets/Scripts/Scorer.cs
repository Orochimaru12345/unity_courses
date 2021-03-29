using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text scoreBoard = null;
    [SerializeField] long collisionCost = 20;
    [SerializeField] long timeCost = 5;

    private long score = 0;

    private void Start()
    {
        StartCoroutine(DegradeScoreOverTime());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (score >= long.MinValue + collisionCost + timeCost)
        {
            score -= collisionCost;
        }
        ShowCurrentScore();
    }

    private IEnumerator DegradeScoreOverTime()
    {
        while (score >= long.MinValue + collisionCost + timeCost)
        {
            yield return new WaitForSeconds(1);
            score -= timeCost;
            ShowCurrentScore();
        }
    }

    private void ShowCurrentScore()
    {
        scoreBoard.text = score.ToString();
    }
}


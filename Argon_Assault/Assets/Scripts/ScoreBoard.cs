using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    int score = 0;
    UnityEngine.UI.Text text;


    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;
        DisplayCurrentScore();
    }

    public void DisplayCurrentScore()
    {
        text.text = score.ToString();
    }

    void Start()
    {
        text = GetComponent<UnityEngine.UI.Text>();
    }
}

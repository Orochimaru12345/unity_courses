using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] Mover player;
    [SerializeField] float countDownTime = 2.2f;

    Text text;
    float elapsedTime = 0;
    bool isCountDownFinished = false;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime < countDownTime)
        {
            string remainingTimeStr = (countDownTime - elapsedTime).ToString();
            text.text = remainingTimeStr.Substring(0, 3);
        }
        else if (!isCountDownFinished)
        {
            text.text = "";
            isCountDownFinished = true;
            player.SetIsMovable(true);
        }
    }
}

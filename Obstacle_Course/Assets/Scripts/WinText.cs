using UnityEngine;
using UnityEngine.UI;

public class WinText : MonoBehaviour
{
    public void ShowWinText()
    {
        GetComponent<Text>().text = "Victory";
    }
}

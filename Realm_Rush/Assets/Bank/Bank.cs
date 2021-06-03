using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;

    [SerializeField] int currentBalance = 0;

    public int CurrentBalance
    {
        get
        {
            return currentBalance;
        }
    }

    [SerializeField] Text balanceDisplay;

    void Awake()
    {
        currentBalance = startingBalance;
        UpdateDisplay();
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateDisplay();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);

        UpdateDisplay();

        if (currentBalance < 0)
        {
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    void UpdateDisplay()
    {
        balanceDisplay.text = $"Gold: {currentBalance}";
    }
}

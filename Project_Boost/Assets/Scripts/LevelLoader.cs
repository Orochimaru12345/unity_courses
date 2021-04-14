using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadLevel(int i)
    {
        SceneManager.LoadScene(i);
    }
}

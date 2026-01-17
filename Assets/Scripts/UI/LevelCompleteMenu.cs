using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteMenu : MonoBehaviour
{
    [Header("Next Level")]
    // Aici este variabila "magica" pe care o poti schimba din Inspector
    public string nextLevelName; 

    public void LoadNextLevel()
    {
        Time.timeScale = 1f; // Repornim timpul
        // Încărcăm scena definită în variabila de mai sus
        SceneManager.LoadScene(nextLevelName);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
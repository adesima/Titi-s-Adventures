using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Variabilă statică pentru a verifica din alte scripturi dacă jocul e pe pauză (opțional)
    public static bool GameIsPaused = false;

    // Referință către obiectul vizual al meniului (Panel-ul)
    public GameObject pauseMenuUI;

    void Update()
    {
        // Verificăm dacă se apasă tasta ESC
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Funcția pentru butonul RESUME
    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Ascunde meniul
        Time.timeScale = 1f;          // Timpul curge normal (1 = viteza normală)
        GameIsPaused = false;
    }

    // Funcția internă de Pauză (activată de ESC)
    void Pause()
    {
        pauseMenuUI.SetActive(true);  // Afișează meniul
        Time.timeScale = 0f;          // Timpul se oprește complet (0 = înghețat)
        GameIsPaused = true;
    }

    // Functia pentru butonul EXIT 
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        
        SceneManager.LoadScene("StartScene");
    }
}
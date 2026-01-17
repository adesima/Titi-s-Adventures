using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathMenuUI;

    // Această funcție va fi apelată din scriptul de viață al jucătorului
    public void TriggerDeath()
    {
        deathMenuUI.SetActive(true); // Arată meniul
        Time.timeScale = 0f;         // Oprește timpul (ca să nu te mai atace inamicii după ce ai murit)
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Important: Repornim timpul!
        // Reîncarcă scena curentă
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
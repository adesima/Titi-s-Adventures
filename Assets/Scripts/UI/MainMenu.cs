using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Această funcție va fi apelată de butonul START
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1"); 
    }

    // Această funcție va fi apelată de butonul EXIT
    public void QuitGame()
    {
        Debug.Log("Jocul se închide..."); 
        Application.Quit();

        // Codul de mai jos este doar pentru a opri jocul în editorul Unity
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
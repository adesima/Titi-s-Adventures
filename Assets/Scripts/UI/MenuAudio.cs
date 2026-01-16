using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    [Header("Surse Audio")]
    public AudioSource sfxSource; // Sursa pentru efecte sonore

    [Header("Clipuri Audio")]
    public AudioClip hoverSound;
    public AudioClip clickSound;

    // Funcție pentru sunetul de HOVER
    public void PlayHover()
    {
        if (hoverSound != null)
        {
            sfxSource.PlayOneShot(hoverSound);
        }
    }

    // Funcție pentru sunetul de CLICK
    public void PlayClick()
    {
        if (clickSound != null)
        {
            sfxSource.PlayOneShot(clickSound);
        }
    }
}
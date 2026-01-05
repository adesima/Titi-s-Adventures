using UnityEngine;
using TMPro; // Necesar pentru a lucra cu TextMeshPro

public class PlayerInventory : MonoBehaviour
{
    // Atributul care numără lemnele
    public int woodCount = 0;

    // Referință către textul de pe ecran
    public TextMeshProUGUI woodText;

    void Start()
    {
        // Actualizăm textul la începutul jocului
        UpdateUI();
    }

    // Funcție publică pe care o va apela lemnul când este colectat
    public void AddWood()
    {
        woodCount++; // Crește numărul
        UpdateUI();  // Actualizează ecranul
    }

    // Funcție separată pentru actualizarea textului
    void UpdateUI()
    {
        if (woodText != null)
        {
            woodText.text = "Lemne: " + woodCount.ToString();
        }
        else
        {
            Debug.LogWarning("Nu ai asignat Text-ul in Inspector la PlayerInventory!");
        }
    }

    public int GetWoodCount()
    {
        return woodCount;
    }
}
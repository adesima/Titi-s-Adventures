using UnityEngine;
using TMPro; 

public class PlayerInventory : MonoBehaviour
{
    
    public int woodCount = 0;

    
    public TextMeshProUGUI woodText;

    void Start()
    {
        
        UpdateUI();
    }

    
    public void AddWood()
    {
        woodCount++; 
        UpdateUI();  
    }

    public void RemoveWood(int amount)
    {
        woodCount -= amount;
        if (woodCount < 0)
            woodCount = 0; 

        UpdateUI(); 
    }

    
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
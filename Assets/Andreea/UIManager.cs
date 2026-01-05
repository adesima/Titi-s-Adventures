using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
  
    private Label healthUI;
    private ProgressBar ProgressBar;
    int health = 100;
    static int item = 0;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        ProgressBar = root.Q<ProgressBar>("viata");
        healthUI = root.Q<Label>("health");
        
        UpdateResourceUI();
    }
    private void FixedUpdate()
    {
       health = HealthBar.GetCurrentHealth();
        UpdateResourceUI();
    }
    public void UpdateResourceUI()
    {
        ProgressBar.value = health;
       // ProgressBar.title = $"{health} / 100";
       //Debug.Log(health);
        //healthUI.text = $"❤️  {health}";
    }
    public static void AddItem(Collider2D Item)
    {
        if (Item.name.Contains("Item"))
        {
            Destroy(Item.gameObject);
            item++; 
        }
    }
}

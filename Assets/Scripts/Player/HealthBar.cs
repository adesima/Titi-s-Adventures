using UnityEngine;

public class HealthBar : MonoBehaviour
{
    static private int _currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentHealth = 100;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }
        Debug.Log("Current Health: " + _currentHealth);
    }

    public void RegenHealth(int regen)
    {
        _currentHealth +=regen;
        if (_currentHealth > 100)
        {
            _currentHealth = 100;
        }
        Debug.Log("Current Health: " + _currentHealth);
    }

    static public int GetCurrentHealth()
        {
        return _currentHealth;
    }

   
}

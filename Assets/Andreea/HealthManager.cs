//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using System.Collections.Generic;

//public class HealthManager : MonoBehaviour
//{
//    public Image healthBar;
//    public float healthAmount = 100f;

//    private void Update()
//    {
//        if(healthAmount <= 0)
//        {
//            Application.LoadLevel(Application.loadedLevel);
//        }
//        if (Input.GetKeyDown(KeyCode.Return))
//        {
//                       TakeDamage(20);
//        }
//        if(Input.GetKeyDown(KeyCode.Space))
//        {
//                       Heal(5);
//        }
//    }
//    public void TakeDamage(float damage)
//    {
//        healthAmount-= damage;
//        healthBar.fillAmount = healthAmount / 100f; 


//    }
//    public void Heal(float healAmount)
//    {
//        healthAmount += healAmount;
//        healthAmount= Mathf.Clamp(healthAmount, 0f, 100);
//        healthBar.fillAmount = healthAmount / 100f;
//    }
//}

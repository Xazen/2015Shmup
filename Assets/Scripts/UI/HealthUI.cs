using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthUI : MonoBehaviour 
{
    [SerializeField]
    private Image healthImage;

    private int? maxHealth;
    public int MaxHealth { set { maxHealth = value; } get { return maxHealth.Value; } }

    private int currentHealth;

    /// <summary>
    /// Reset the health back to max health
    /// </summary>
    public void Reset()
    {
        if (maxHealth.HasValue)
        {
            currentHealth = maxHealth.Value;
            UpdateHealthUI();
        }
        else
        {
            Debug.LogError("You need to set max Health first!", this);
        }
    }

    /// <summary>
    /// Decrease the health
    /// </summary>
    /// <param name="value">The amount of health that should be reduced</param>
    public void DecreaseHealth(int value)
    {
        currentHealth = Mathf.Max(currentHealth - value, 0);
        UpdateHealthUI();
    }

    /// <summary>
    /// Increase the health
    /// </summary>
    /// <param name="value">The amount of health that should be added</param>
    public void IncreaseHealth(int value)
    {
        currentHealth = Mathf.Min(currentHealth + value, 1);
        UpdateHealthUI();
    }

    /// <summary>
    /// Update the health ui
    /// </summary>
    private void UpdateHealthUI()
    {
        healthImage.fillAmount = (float)((float)currentHealth / (float)maxHealth);
    }
}

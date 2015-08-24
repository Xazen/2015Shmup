using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour 
{
    // Variables
    [SerializeField]
    private int maxLife = 5;
    private int currentLife;

    // UI
    [SerializeField]
    private HealthUI healthUI;

    // Delegate
    public delegate void PlayerHealthDepleted();
    public event PlayerHealthDepleted healthDepleted;

	// Use this for initialization
	void Start () 
    {
        ResetLife();
    }

    /// <summary>
    /// Decreases life
    /// </summary>
    /// <param name="value">Value to decrease life</param>
    public void DecreaseLife(int value)
    {
        currentLife -= Mathf.Min(value, currentLife);
        healthUI.DecreaseHealth(value);

        if (currentLife <= 0 && healthDepleted != null)
        {
            healthDepleted();
        }
    }

    /// <summary>
    /// Increases life
    /// </summary>
    /// <param name="value">Value to decreases life</param>
    public void IncreaseLife(int value)
    {
        currentLife += Mathf.Min(value, maxLife-currentLife);
        healthUI.IncreaseHealth(value);
    }

    /// <summary>
    /// Reset life back to maximum
    /// </summary>
    public void ResetLife()
    {
        healthUI.MaxHealth = maxLife;
        healthUI.Reset();

        currentLife = maxLife;
    }
}

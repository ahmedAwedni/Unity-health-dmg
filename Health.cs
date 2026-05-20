// Health.cs
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float _currentHealth;

    [Header("Defense Settings")]
    [Tooltip("Reduces incoming damage by a flat amount before it hits the health pool.")]
    [SerializeField] private float defense = 0f;

    [Header("Regeneration Settings")]
    [Tooltip("If true, the object will slowly heal over time after taking damage.")]
    [SerializeField] private bool enableRegen = false;
    [Tooltip("How much health to restore per tick.")]
    [SerializeField] private float regenAmount = 2f;
    [Tooltip("Time in seconds between each regeneration tick.")]
    [SerializeField] private float regenInterval = 1f;
    [Tooltip("How long to wait after taking damage before regeneration starts.")]
    [SerializeField] private float regenDelay = 3f;

    private Coroutine regenCoroutine;

    [Header("Events")]
    public UnityEvent<float> onHealthChanged;
    public UnityEvent onDamageTaken;
    public UnityEvent onDamageBlocked; // Fires when defense absorbs all the damage
    public UnityEvent onHealed;
    public UnityEvent onDeath;

    public float CurrentHealth => _currentHealth;
    public bool IsDead => _currentHealth <= 0;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float amount) /// Reduces health by a specified amount.
    {
        if (IsDead || amount <= 0) return;

        // Apply defense reduction
        float effectiveDamage = amount - defense;

        // Check if the armor completely blocked the hit
        if (effectiveDamage <= 0)
        {
            onDamageBlocked?.Invoke();
            return;
        }

        _currentHealth -= effectiveDamage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);

        onDamageTaken?.Invoke();
        onHealthChanged?.Invoke(_currentHealth / maxHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }
        else if (enableRegen)
        {
            // Restart the regeneration timer if we took damage but didn't die
            if (regenCoroutine != null) StopCoroutine(regenCoroutine);
            regenCoroutine = StartCoroutine(RegenRoutine());
        }
    }

    public void Heal(float amount) /// Increases health by a specified amount.
    {
        if (IsDead || amount <= 0) return;

        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);

        onHealed?.Invoke();
        onHealthChanged?.Invoke(_currentHealth / maxHealth);
    }

    // Optional: A public method to adjust defense dynamically (e.g., picking up a shield)
    public void AddDefense(float amount)
    {
        defense += amount;
    }

    private IEnumerator RegenRoutine()
    {
        // Wait for the delay period before starting to heal
        yield return new WaitForSeconds(regenDelay);

        // Keep healing as long as the object isn't dead and isn't at max health
        while (!IsDead && _currentHealth < maxHealth)
        {
            // We use the existing Heal method so the UI events fire automatically!
            Heal(regenAmount);
            yield return new WaitForSeconds(regenInterval);
        }

        // Clean up the reference when we finish regenerating
        regenCoroutine = null;
    }

    private void Die()
    {
        onDeath?.Invoke();
        Debug.Log($"{gameObject.name} has died.");
        
        // Stop regeneration if the object dies
        if (regenCoroutine != null)
        {
            StopCoroutine(regenCoroutine);
            regenCoroutine = null;
        }
        
        // add death logic here (e.g., disable components, play animation, etc.)
    }
}

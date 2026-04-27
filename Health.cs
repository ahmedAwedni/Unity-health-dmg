// Health.cs
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

    private void Die()
    {
        onDeath?.Invoke();
        Debug.Log($"{gameObject.name} has died.");
        // add death logic here (e.g., disable components, play animation, etc.)
    }
}

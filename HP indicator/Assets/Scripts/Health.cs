using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealthPoints = 100;
    private int _currentHealthPoints;

    public event Action Died;
    public event Action HealthChanged;

    public int MaxValue => _maxHealthPoints;
    public int CurrentValue => _currentHealthPoints;
    public bool IsAlive => _currentHealthPoints > 0;

    private void Awake()
    {
        _currentHealthPoints = _maxHealthPoints;
    }

    public void SetMaxHealth(int maxHealth)
    {
        _maxHealthPoints = maxHealth;

        if (_currentHealthPoints > _maxHealthPoints)
        {
            _currentHealthPoints = _maxHealthPoints;
        }
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0 || !IsAlive)
        {
            return;
        }        

        _currentHealthPoints = Mathf.Max(0, _currentHealthPoints - damage);
        HealthChanged?.Invoke();

        if (_currentHealthPoints == 0)
        {
            Die();
        }
    }

    public void RestoreHealth(int amount)
    {
        if (amount <= 0 || !IsAlive)
        {
            return;
        }

        int healed = Mathf.Min(amount, _maxHealthPoints - _currentHealthPoints);
        _currentHealthPoints += healed;
        HealthChanged?.Invoke();
    }

    private void Die()
    {
        Died?.Invoke();
        Debug.Log($"{name} погиб!");
        gameObject.SetActive(false);
    }
}
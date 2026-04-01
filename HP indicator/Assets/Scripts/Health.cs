using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHP = 100;
    
    private int _currentHP;

    public int MaxHealth => _maxHP;
    public int CurrentHealth => _currentHP;
    public bool IsAlive => _currentHP > 0;

    public event Action Died;
    public event Action<int> Damaged;
    public event Action<int> Healed;

    private void Awake()
    {
        _currentHP = _maxHP;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0 || !IsAlive) return;

        _currentHP = Mathf.Max(0, _currentHP - damage);
        Damaged?.Invoke(damage);

        if (_currentHP == 0)
        {
            Die();
        }
    }

    public void RestoreHealth(int amount)
    {
        if (amount <= 0 || !IsAlive) return;

        int healed = Mathf.Min(amount, _maxHP - _currentHP);
        _currentHP += healed;
        Healed?.Invoke(healed);
    }

    private void Die()
    {
        Died?.Invoke();
        Debug.Log($"{name} погиб!");
        gameObject.SetActive(false);
    }

    public void SetMaxHealth(int maxHealth)
    {
        _maxHP = maxHealth;
        if (_currentHP > _maxHP)
        {
            _currentHP = _maxHP;
        }
    }
}
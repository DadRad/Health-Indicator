using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxValue = 100;
    private int _currentValue;

    public event Action Died;
    public event Action ValueChanged;

    public int MaxValue => _maxValue;
    public int CurrentValue => _currentValue;
    public bool IsAlive => _currentValue > 0;

    private void Awake()
    {
        _currentValue = _maxValue;
    }

    public void SetMaxValue(int maxValue)
    {
        _maxValue = maxValue;

        if (_currentValue > _maxValue)
        {
            _currentValue = _maxValue;
        }
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0 || !IsAlive)
        {
            return;
        }        

        _currentValue = Mathf.Max(0, _currentValue - damage);
        ValueChanged?.Invoke();

        if (_currentValue == 0)
        {
            Die();
        }
    }

    public void RestoreValue(int amount)
    {
        if (amount <= 0 || !IsAlive)
        {
            return;
        }

        int healed = Mathf.Min(amount, _maxValue - _currentValue);
        _currentValue += healed;
        ValueChanged?.Invoke();
    }

    private void Die()
    {
        Died?.Invoke();
        Debug.Log($"{name} погиб!");
        gameObject.SetActive(false);
    }
}
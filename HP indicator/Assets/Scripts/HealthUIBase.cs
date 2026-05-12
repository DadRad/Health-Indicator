using UnityEngine;

public abstract class HealthUIBase : MonoBehaviour
{
    [SerializeField] protected Health _health;

    protected virtual void OnEnable()
    {
        if (_health != null)
        {
            _health.ValueChanged += OnHealthChanged;
        }
    }

    protected virtual void OnDisable()
    {
        if (_health != null)
        {
            _health.ValueChanged -= OnHealthChanged;
        }
    }

    protected virtual void Start()
    {
        OnHealthChanged();
    }

    protected abstract void OnHealthChanged();

    protected float GetCurrentFill()
    {
        if (_health.MaxValue <= 0) return 0f;
        return (float)_health.CurrentValue / _health.MaxValue;
    }
}

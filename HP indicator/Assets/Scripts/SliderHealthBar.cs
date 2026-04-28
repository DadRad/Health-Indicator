using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderHealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;

    private Slider _slider;
    private float _currentFillValue;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _health.HealthChanged += UpdateBar;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= UpdateBar;
    }

    private void Start()
    {
        _currentFillValue = GetCurrentFill();
        UpdateBar();
    }

    public void UpdateBar()
    {
        _currentFillValue = GetCurrentFill();
        _slider.value = _currentFillValue;
    }

    private float GetCurrentFill()
    {
        if (_health.MaxHealth <= 0) return 0f;
        return (float)_health.CurrentHealth / _health.MaxHealth;
    }
}

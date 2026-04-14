using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _smoothSpeed = 0.1f;

    private Slider _slider;
    private bool _isMoving;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        UpdateBarSmoothly();
    }

    private void OnEnable()
    {
        _health.HealthChanged += UpdateBarSmoothly;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= UpdateBarSmoothly;
    }

    public void UpdateBarSmoothly()
    {
        if (_isMoving)
        {
            StopCoroutine(SmoothMove());
        }

        StartCoroutine(SmoothMove());
    }

    private IEnumerator SmoothMove()
    {
        _isMoving = true;

        float currentValue = _slider.value;
        float targetValue = _health.CurrentHealth;

        while (Mathf.Abs(currentValue - targetValue) > 0.01f)
        {
            currentValue = Mathf.MoveTowards(currentValue, targetValue, _smoothSpeed * Time.deltaTime * 100);
            _slider.value = currentValue;

            yield return null;
        }

        _slider.value = targetValue;
        _isMoving = false;
    }
}

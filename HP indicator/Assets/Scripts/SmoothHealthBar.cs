using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SmoothHealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _smoothSpeed = 0.1f;

    private float _snapThreshold = 0.01f;
    private Slider _slider;
    private Coroutine _smoothMoveCoroutine;
    private float _currentFillValue;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _health.HealthChanged += UpdateBarSmoothly;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= UpdateBarSmoothly;
    }

    private void Start()
    {
        _slider.value = GetCurrentFill();
    }

    public void UpdateBarSmoothly()
    {
        if (_smoothMoveCoroutine != null)
        {
            StopCoroutine(_smoothMoveCoroutine);
        }

        _smoothMoveCoroutine = StartCoroutine(SmoothMove());
    }

    private IEnumerator SmoothMove()
    {
        float targetValue = GetCurrentFill();

        while (Mathf.Abs(_currentFillValue - targetValue) > _snapThreshold)
        {
            _currentFillValue = Mathf.MoveTowards(_currentFillValue,targetValue,  _smoothSpeed * Time.deltaTime);
            _slider.value = _currentFillValue;
            yield return null;
        }

        _slider.value = targetValue;
        _currentFillValue = targetValue;
    }
    
    private float GetCurrentFill()
    {
        if (_health.MaxValue <= 0) return 0f;
        return (float)_health.CurrentValue / _health.MaxValue;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SliderHealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _smoothSpeed = 0.1f;

    private Slider _slider;
    private bool _isMoving;

    public void UpdateBar()
    {
        StopAllCoroutines();
        _isMoving = false;
        _slider.value = _health.CurrentHealth;
    }

    public void UpdateBarSmoothly()
    {
        if (_isMoving)
        {
            StopAllCoroutines();
        }

        StartCoroutine(SmoothMove());
    }

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        UpdateBar();
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

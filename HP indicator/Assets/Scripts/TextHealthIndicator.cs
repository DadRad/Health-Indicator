using TMPro;
using UnityEngine;

public class TextHealthIndicator : MonoBehaviour
{
    [SerializeField] private Health _health;

    private TextMeshProUGUI _text;

    public void UpdateHP()
    {
        _text.text = _health.CurrentHealth.ToString() + "/" + _health.MaxHealth;
        
    }

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateHP();
    }

    private void OnEnable()
    {
        _health.HealthChanged += UpdateHP;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= UpdateHP;
    }
}

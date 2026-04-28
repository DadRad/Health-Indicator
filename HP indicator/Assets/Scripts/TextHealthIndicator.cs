using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextHealthIndicator : MonoBehaviour
{
    [SerializeField] private Health _health;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _health.HealthChanged += UpdateHP;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= UpdateHP;
    }

    private void Start()
    {
        UpdateHP();
    }
    
    public void UpdateHP()
    {
        _text.text = _health.CurrentHealth.ToString() + "/" + _health.MaxHealth;
        
    }
}

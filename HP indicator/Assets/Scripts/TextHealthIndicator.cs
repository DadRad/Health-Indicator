using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextHealthIndicator : HealthUIBase
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    protected override void OnHealthChanged()
    {
        _text.text = _health.CurrentValue.ToString() + "/" + _health.MaxValue;
    }
}

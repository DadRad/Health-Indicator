using TMPro;
using UnityEngine;

public class TextHealthIndicator : MonoBehaviour
{
    [SerializeField] Health _health;

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
}

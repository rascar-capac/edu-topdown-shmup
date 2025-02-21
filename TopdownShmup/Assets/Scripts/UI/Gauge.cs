using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    [SerializeField] private Image _fill;
    [SerializeField] private FloatInRange _data;

    private void SetFill(float amount)
    {
        _fill.fillAmount = amount;
    }

    private void Data_OnValueChanged(float newValue, float ratio)
    {
        SetFill(ratio);
    }

    private void Awake()
    {
        _data.OnValueChanged.AddListener(Data_OnValueChanged);

        SetFill(_data.CurrentRatio);
    }

    private void OnDestroy()
    {
        _data.OnValueChanged.RemoveListener(Data_OnValueChanged);
    }
}

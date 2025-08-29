using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class MoneyDisplay : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        MoneyManager.Instance.AmountUpdated.AddListener(OnAmountUpdate);
    }
    private void OnDestroy()
    {
        MoneyManager.Instance.AmountUpdated.RemoveListener(OnAmountUpdate);
    }

    private void OnAmountUpdate(int amount)
    {
        _textMesh.text = amount.ToString();
    }
}

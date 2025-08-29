using UnityEngine;
using UnityEngine.UI;

public class SelectableButton : MonoBehaviour
{
    [SerializeField] 
    private GameObject Prefab;  

    private Button _button;

    void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnSelect);
    }

    void OnSelect()
    {
        SelectionManager.Instance.SetSelectedPrefab(Prefab);
    }
}

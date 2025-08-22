using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; private set; }

    private GameObject _selectedPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SetSelectedPrefab(GameObject prefab)
    {
        _selectedPrefab = prefab;
    }

    public GameObject GetSelectedPrefab()
    {
        return _selectedPrefab;
    }
}

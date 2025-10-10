using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealthUI : MonoBehaviour
{
    [SerializeField]
    private List<Image> Lives;

    public void UpdateLives(int lives)
    {
        Debug.Log(lives);
        Lives[lives].enabled = false;
    }
}

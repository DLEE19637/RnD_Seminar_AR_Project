using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRevealer : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _hiddenButtons;

    private bool _toggle;

    private void Start()
    {
        _toggle = false;
    }

    public void RevealButtons()
    {
        _toggle = !_toggle;

        Console.Write("Here");
        foreach (var button in _hiddenButtons)
        {
            button.SetActive(_toggle);
        }
    }
}

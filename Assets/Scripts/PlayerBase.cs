using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.RegisterBase(this);
    }
}

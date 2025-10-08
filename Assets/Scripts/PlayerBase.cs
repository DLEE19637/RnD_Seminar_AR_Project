using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public int Health;
    void Start()
    {
        GameManager.Instance.RegisterBase(this);
    }
}

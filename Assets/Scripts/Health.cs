using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    public UnityEvent Died;

    public void Damage(int damage)
    {
        if (CurrentHealth > 0 && damage >= CurrentHealth)
        {
            Died.Invoke();
        }
        CurrentHealth -= damage;
    }

    public void SetMaxHealth(int health)
    {
        MaxHealth = health;
        CurrentHealth = health;
    }

}

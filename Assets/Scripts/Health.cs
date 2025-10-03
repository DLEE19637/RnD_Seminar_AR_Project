using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private Image _healthBarImage;
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }

    public UnityEvent Died;
    public UnityEvent HealEvent;
    public UnityEvent TookDamage;

    public void Damage(float damage)
    {
        if (CurrentHealth > 0 && damage >= CurrentHealth)
        {
            Died.Invoke();
        }
        TookDamage.Invoke();
        CurrentHealth -= damage;
        UpdateHealthBar();
    }

    public void Heal(float heal)
    {
        if (CurrentHealth + heal < MaxHealth)
        {
            CurrentHealth += heal;
        }
        UpdateHealthBar();
    }

    public void SetMaxHealth(int health)
    {
        MaxHealth = health;
        CurrentHealth = health;
    }

    private void UpdateHealthBar()
    {
        _healthBarImage.fillAmount = CurrentHealth / (float)MaxHealth;
    }

}

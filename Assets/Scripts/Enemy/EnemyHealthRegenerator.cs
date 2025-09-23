using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemyHealthRegenerator : MonoBehaviour
{
    private EnemyController _enemyController;
    private Health _health;
    [SerializeField]
    private int _regenerationAmount;
    [SerializeField]
    private float _cooldown;

    private float _timeSinceLastDamage;
    void Start()
    {
        _enemyController = GetComponent<EnemyController>();

        _health = GetComponent<Health>();   
        _health.TookDamage.AddListener(OnDamageTaken);
    }

    void Update()
    {
        _timeSinceLastDamage += Time.deltaTime;
        if (_timeSinceLastDamage >= _cooldown)
        {
            _health.Heal(_regenerationAmount * Time.deltaTime);
        }
    }

    public void OnDamageTaken()
    {
        _timeSinceLastDamage = 0f;
    }
}

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemySpeedDeviation : MonoBehaviour
{
    private EnemyController _enemyController;
    [SerializeField] 
    private float _boostMultiplier;
    [SerializeField] 
    private float _boostDuration;

    private bool _boosted = false;
    private float _originalSpeed;

    void Start()
    {
        _enemyController = GetComponent<EnemyController>();

        var health = GetComponent<Health>();
        health.TookDamage.AddListener(OnTookDamage);

        _originalSpeed = _enemyController.Speed;
    }

    private void OnTookDamage()
    {
        if (!_boosted)
        {
            StartCoroutine(BoostCoroutine());
            _boosted = true;
        }
    }
    private IEnumerator BoostCoroutine()
    {
        _enemyController.Speed *= _boostMultiplier;

        yield return new WaitForSeconds(_boostDuration);

        _enemyController.Speed = _originalSpeed;
    }
}

using UnityEngine;
using UnityEngine.Events;

public class MoneyManager : MonoBehaviour
{
    private int _currentMoney;

    public UnityEvent<int> AmountUpdated = new();

    private static MoneyManager _instance;
    public static MoneyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject newObject = new("MoneyManager");
                _instance = newObject.AddComponent<MoneyManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    public bool IsAmountAvailable(int amount)
    {
        return _currentMoney >= amount;
    }

    public bool Spend(int amount)
    {
        if (!IsAmountAvailable(amount))
        {
            return false;
        }

        _currentMoney -= amount;
        return true;
    }

    public void SubscribeEnemy(EnemyController enemyController)
    {
        enemyController.Died.AddListener(OnEnemyDeath);
    }

    private void OnEnemyDeath(EnemyController enemyController)
    {
        _currentMoney += enemyController.EnemyData.KillReward;
    }
}

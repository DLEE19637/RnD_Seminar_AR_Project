using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private EnemyData EnemyData;

    public int Health;
    public float Speed;

    void Start()
    {
        Health = EnemyData.Health;
        Speed = EnemyData.Speed;
    }

    void Update()
    {
        Vector3 direction = GameManager.Instance.Base.transform.position - transform.position;
        transform.Translate(direction.normalized * EnemyData.Speed * Time.deltaTime, Space.World);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Destroyed();
        }
    }

    public void Destroyed()
    {
        //Misschien hier behaviour zetten van cash krijgen, death sound effect, etc.
        Destroy(gameObject);
    }
}

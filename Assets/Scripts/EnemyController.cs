using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private EnemyData EnemyData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = GameManager.Instance.Base.transform.position - transform.position;
        transform.Translate(direction.normalized * EnemyData.Speed * Time.deltaTime, Space.World);
    }
}

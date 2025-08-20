using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public enum GameState { Waiting, Playing, Paused, GameOver }
    public GameState CurrentState { get; private set; }
    public GameObject Base;

    public void SetState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log("Game state changed to: " + newState);
    }
}

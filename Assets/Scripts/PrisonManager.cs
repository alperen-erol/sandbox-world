using UnityEngine;
using System.Collections.Generic;

public class PrisonManager : MonoBehaviour
{
    public static PrisonManager Instance;

    [SerializeField] float winConditionEnemyCount;
    private List<Enemy> capturedEnemies = new List<Enemy>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Update()
    {
    }


    public void EnemyCaptured(Enemy enemy)
    {
        Debug.Log("Enemy Captured");
        if (!capturedEnemies.Contains(enemy))
        {
            capturedEnemies.Add(enemy);
            CheckForGameEnd();
        }
    }


    public void EnemyReleased(Enemy enemy)
    {
        Debug.Log("Enemy Released");
        capturedEnemies.Remove(enemy);

    }


    private void CheckForGameEnd()
    {
        if (capturedEnemies.Count >= winConditionEnemyCount)
        {
            EndGame();
        }
    }


    private void EndGame()
    {
        GameManager.Instance.HandlePlayerWin();
    }
}
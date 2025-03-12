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
        Debug.Log(capturedEnemies.Count);
    }


    // Called when an enemy enters ANY cell for the first time
    public void EnemyCaptured(Enemy enemy)
    {
        Debug.Log("Enemy Captured");
        if (!capturedEnemies.Contains(enemy))
        {
            capturedEnemies.Add(enemy);
            CheckForGameEnd();
        }
    }


    // Called when an enemy exits ALL cells
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
        Debug.Log("Game Over! All enemies captured.");
        // Add your game-end logic here
    }
}
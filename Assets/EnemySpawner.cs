using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;


    [Header("Spawn Area")]
    [SerializeField] private Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f);
    // [SerializeField] private float edgeBuffer = 1f;
    [SerializeField] private LayerMask obstacleLayerMask;
    [SerializeField] private float minDistanceFromObstacles = 1f;

    [Header("Enemy")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnHeight = 0.5f;
    public float enemyRandomSpeedMin, EnemyRandomSpeedMax;

    [Header("Round Settings")]
    [SerializeField] private int initialEnemyCount = 5;
    [SerializeField] private int enemyIncreasePerRound = 5;

    [SerializeField] TMP_Text roundEnemiesText;

    private int currentRound = 0;
    private int enemiesRemaining = 0;
    private List<GameObject> activeEnemies = new List<GameObject>();


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }



    private void Start()
    {
        StartNextRound();
    }

    private void Update()
    {
        CleanupDestroyedEnemies();
        if (enemiesRemaining <= 0 && activeEnemies.Count == 0)
        {
            StartNextRound();
        }
        roundEnemiesText.text = "Round: " + currentRound + " Enemies Remaining: " + enemiesRemaining;
    }

    private void CleanupDestroyedEnemies()
    {
        activeEnemies.RemoveAll(enemy => enemy == null);
        enemiesRemaining = activeEnemies.Count;
    }

    private void StartNextRound()
    {
        currentRound++;
        int enemiesToSpawn = initialEnemyCount + (currentRound - 1) * enemyIncreasePerRound;

        Debug.Log("Starting Round " + currentRound + " with " + enemiesToSpawn + " enemies");

        StartCoroutine(SpawnEnemiesWithDelay(enemiesToSpawn));

        enemiesRemaining = activeEnemies.Count;
    }

    private IEnumerator SpawnEnemiesWithDelay(int count)
    {
        
        for (int i = 0; i < count; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }

        enemiesRemaining = activeEnemies.Count;
    }

    IEnumerator wait1s()
    {
        yield return new WaitForSeconds(0.5f);
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = FindValidSpawnPosition();
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        activeEnemies.Add(enemy);
    }

    private Vector3 FindValidSpawnPosition()
    {
        Vector3 spawnPos;
        int maxAttempts = 30;
        int attempts = 0;

        do
        {
            float xPos = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
            float zPos = Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2);

            spawnPos = transform.position + new Vector3(xPos, spawnHeight, zPos);

            attempts++;

            if (IsValidSpawnPosition(spawnPos) || attempts >= maxAttempts)
                break;

        } while (true);

        if (attempts >= maxAttempts)
        {
            Debug.LogWarning(maxAttempts + " denemede bulunamadi");
        }

        return spawnPos;
    }

    private bool IsValidSpawnPosition(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, minDistanceFromObstacles, obstacleLayerMask);

        return hitColliders.Length == 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);

        Gizmos.color = Color.yellow;
        Vector3 innerSize = new Vector3(spawnAreaSize.x * 2, spawnAreaSize.y, spawnAreaSize.z * 2);
        Gizmos.DrawWireCube(transform.position, innerSize);
    }
}
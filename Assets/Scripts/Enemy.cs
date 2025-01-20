using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyHealth = 100f;

    private void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}

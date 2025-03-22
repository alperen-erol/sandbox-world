using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float enemyHealth;
    [SerializeField] float drainRate;
    public bool isDrain = false;


    void Update()
    {
        DrainEnemyHealth(isDrain);
    }

    public void DrainEnemyHealth(bool isDrain)
    {
        if (isDrain)
            enemyHealth -= Time.deltaTime * drainRate;
        if (enemyHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

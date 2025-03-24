using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth;
    public float gatherSoulRate;
    [SerializeField] ParticleSystem SoulParticles;
    public bool isDrain = false, isDead = false;


    void Update()
    {
        if (isDrain)
            DrainEnemyHealth(gatherSoulRate);
    }


    public void StartSoulDrain()
    {
        SoulParticles.Play();
    }
    public void StopSoulDrain()
    {
        SoulParticles.Stop();
    }


    public void DrainEnemyHealth(float gatherSoulRate)
    {
        enemyHealth -= Time.deltaTime * gatherSoulRate;
        if (enemyHealth <= 0)
        {
            isDead = true;
            Destroy(this.gameObject);
        }
    }
}

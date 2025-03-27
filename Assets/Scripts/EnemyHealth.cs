using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] ParticleSystem SoulParticles;
    public float enemyHealth;
    public float gatherSoulRate;
    public float hammerDamage;
    public float hammerDamageCooldown;
    public float hammerDamageCooldownTimer;
    public bool isDrain = false, isDead = false, isEnemyHit = false;

    public TMP_Text healthText;
    public Transform healthBarCanvas;
    public Transform mainCamera;


    void Start()
    {
        mainCamera = Camera.main.transform;
    }


    void Update()
    {
        if (isDrain)
            DrainEnemyHealth(gatherSoulRate);
        healthText.text = Mathf.RoundToInt(enemyHealth).ToString();
        healthBarCanvas.LookAt(mainCamera);
        if (enemyHealth <= 0)
        {
            isDead = true;
            Destroy(this.gameObject);
        }


        hammerDamageCooldownTimer -= Time.deltaTime;
        if (hammerDamageCooldownTimer < 0)
        {
            isEnemyHit = false;
        }
        else
            isEnemyHit = true;
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

    }

    public void TakeHammerDamage(float hammerDamage)
    {
        hammerDamage = hammerDamage;
        if (hammerDamageCooldownTimer <= 0)
        {
            enemyHealth -= hammerDamage;
            hammerDamageCooldownTimer = hammerDamageCooldown;
        }
    }
}

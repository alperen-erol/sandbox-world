using TMPro;
using UnityEditor;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] ParticleSystem SoulParticles;
    Fracture fracture;
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
        fracture = GetComponent<Fracture>();
    }


    void Update()
    {
        if (isDrain)
            DrainEnemyHealth(gatherSoulRate);
        healthText.text = Mathf.RoundToInt(enemyHealth).ToString();
        healthBarCanvas.LookAt(mainCamera);
        if (enemyHealth <= 0.9)
        {
            PlayerInventory.Instance.money += 50;
            isDead = true;
            fracture.ComputeFracture();
            // Destroy(this.gameObject);
        }


        hammerDamageCooldownTimer -= Time.deltaTime;
        if (hammerDamageCooldownTimer < 0)
        {
            isEnemyHit = false;
        }
        else
            isEnemyHit = true;
    }


    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        Debug.Log("ENEMY TOOK " + damage + " DAMAGE");
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

using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip hitsound;
    [SerializeField] Animator animator;
    [SerializeField] TMP_Text healthText;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShotgunAmmo"))
        {
            ShotgunManager.Instance.ammoCount += 3;
            Destroy(other.gameObject);
        }
    }


    void Update()
    {
        if (health <= 0)
        {
            GameManager.Instance.HandlePlayerDeath();
        }
        healthText.text = "Health: " + health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        audioSource.PlayOneShot(hitsound);
        // animator.SetTrigger("TakeDamage");
    }
}

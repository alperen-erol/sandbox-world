using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource audioSourceOnDeath;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip hitsound;
    [SerializeField] Animator animator;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] TMP_Text healthText;
    [SerializeField] GameObject Camera;

    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (health <= 0)
        {
            Die();
        }
        healthText.text = "Health: " + health;
    }

    private void Die()
    {
        gameOverScreen.SetActive(true);

        audioSourceOnDeath.PlayOneShot(deathSound);
        Debug.Log("ded");
        Destroy(gameObject);
        Destroy(Camera);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        audioSource.PlayOneShot(hitsound);
        animator.SetTrigger("TakeDamage");
    }
}

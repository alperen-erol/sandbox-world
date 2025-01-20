using UnityEngine;

public class WeaponAnim : MonoBehaviour
{
    public Animator animator;
    public Enemy enemy;
    public float cooldownTime = 1f;
    public bool isOnCooldown = false;
    private Collider weaponCollider;
    public AudioSource soundSystem;
    public AudioClip swordSwing;
    public AudioClip swordHit;

    private void Start()
    {
        weaponCollider = GetComponent<Collider>();
        weaponCollider.enabled = false; // Ensure it's off at start
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isOnCooldown)
        {
            Attack();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        isOnCooldown = true;
        soundSystem.PlayOneShot(swordSwing);
        Invoke("ResetCooldown", cooldownTime);
    }

    private void EnableCollider()
    {
        weaponCollider.enabled = true;
    }

    private void DisableCollider()
    {
        weaponCollider.enabled = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && weaponCollider.enabled)
        {
            Debug.Log("collider");
            soundSystem.PlayOneShot(swordHit);
            enemy.enemyHealth -= 50f;
        }
    }

    private void ResetCooldown()
    {
        isOnCooldown = false;
    }
}

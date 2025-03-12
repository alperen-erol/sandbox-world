using UnityEngine;

public class WeaponAnim : MonoBehaviour
{
    public Animator animator;
    public AudioSource soundSystem;
    public AudioClip swordSwing;
    public AudioClip swordHit;

    private Collider weaponCollider;
    private int comboStep = 0;
    private bool canAttack = true;
    private bool isAttacking = false;
    public bool isBlocking = false;
    private float comboResetTime = 1.0f;
    private float lastClickTime = 0f;
    private bool canReset = false;

    private void Start()
    {
        weaponCollider = GetComponent<Collider>();
        weaponCollider.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack && !isBlocking)
        {
            PerformCombo();
            lastClickTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartBlocking();
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            StopBlocking();
        }

        if (Time.time - lastClickTime > comboResetTime && canReset && !isBlocking && !isAttacking)
        {
            ResetComboStep();
            canReset = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy!");
            // other.gameObject.GetComponent<Enemy>().enemyHealth -= 50;
            soundSystem.PlayOneShot(swordHit);
        }
    }

    private void PerformCombo()
    {
        if (isAttacking) return;

        canAttack = false;
        isAttacking = true;
        comboStep++;

        if (comboStep > 3)
        {
            comboStep = 2;
        }

        animator.SetTrigger("Attack" + comboStep);
        soundSystem.PlayOneShot(swordSwing);
        lastClickTime = Time.time;
        canReset = true;
    }

    public void ResetComboState()
    {
        canAttack = true;
        isAttacking = false;
    }

    public void ResetComboStep()
    {
        Debug.Log("reset combo step");
        animator.SetTrigger("Idle");
        comboStep = 0;
    }

    public void EnableWeaponCollider()
    {
        weaponCollider.enabled = true;
    }

    public void DisableWeaponCollider()
    {
        weaponCollider.enabled = false;
    }


    private void StartBlocking()
    {
        isBlocking = true;
        animator.SetTrigger("Block");
        comboStep = 0;
    }

    private void StopBlocking()
    {
        isBlocking = false;
        animator.SetTrigger("Idle");
        comboStep = 0;
        canAttack = true;
        isAttacking = false;
    }
}

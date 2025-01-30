using UnityEngine;

public class WeaponAnim : MonoBehaviour
{
    public Animator animator;
    public AudioSource soundSystem;
    public AudioClip swordSwing;
    public AudioClip swordHit;

    private Collider weaponCollider;
    private int comboStep = 0;
    private bool isHoldingAttack = false;

    private void Start()
    {
        weaponCollider = GetComponent<Collider>();
        weaponCollider.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isHoldingAttack = true;
            PerformCombo();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isHoldingAttack = false;
            ResetCombo();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit enemy!");
            other.gameObject.GetComponent<Enemy>().enemyHealth -= 50;
            soundSystem.PlayOneShot(swordHit);
        }
    }

    private void PerformCombo()
    {
        if (!isHoldingAttack) return;

        comboStep++;

        if (comboStep > 3)
        {
            comboStep = 2; // Loop between Attack2 and Attack3
        }

        animator.SetTrigger("Attack" + comboStep);
        soundSystem.PlayOneShot(swordSwing);

        float animationTime = GetAnimationLength("Attack" + comboStep);
        Invoke(nameof(PerformCombo), animationTime * 0.95f); // Auto-continue combo
    }

    private void EnableWeaponCollider()
    {
        weaponCollider.enabled = true;
        Debug.Log("Weapon collider enabled");
    }

    private void DisableWeaponCollider()
    {
        weaponCollider.enabled = false;
        Debug.Log("Weapon collider disabled");
    }

    private void ResetCombo()
    {
        comboStep = 0;
        animator.SetTrigger("Idle");
    }

    private float GetAnimationLength(string animationName)
    {
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        foreach (AnimationClip clip in ac.animationClips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }
        return 0.5f; // Default value
    }
}
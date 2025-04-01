using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    Animator animator;
    private int comboStep = 0;
    private float comboTimer = 0f;
    private float comboWindow = 0.3f;
    private bool isAttacking = false;

    new BoxCollider collider;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] hitSoundEffects;
    [SerializeField] AudioClip[] swingSoundEffects;
    [SerializeField] AudioClip bonk;
    [SerializeField] TMP_Text durabilityText;
    [SerializeField] Animator camAnimator;
    public float hammerDamage;
    public float animSpeed;
    public float HammerCooldown = 0.5f, hammerPanTiltspeed;
    public float HammerCooldownTimer;
    public float durability;

    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider>();
        collider.enabled = false;
    }


    void Update()
    {
        animator.SetFloat("AnimSpeed", AxeManager.Instance.axeAttackSpeed);
        hammerDamage = AxeManager.Instance.axeAttackDamage;
        if (durability <= 0)
        {
            durabilityText.text = "Weapon Broken";
        }
        else
        {
            durabilityText.text = "Durability: " + durability;
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Attack");
            animator.SetBool("Combo1", true);
            animator.SetBool("Combo2", true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            animator.SetBool("Combo1", false);
            animator.SetBool("Combo2", false);
        }


        if (durability <= 0)
        {
            this.gameObject.SetActive(false);
        }
        if (durability > 0)
            this.gameObject.SetActive(true);
    }

    void EnableWeaponCollider()
    {
        collider.enabled = true;
    }

    void DisableWeaponCollider()
    {
        collider.enabled = false;
    }

    void PlaySwooshSound()
    {
        audioSource.PlayOneShot(swingSoundEffects[Random.Range(0, 2)]);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAttack"))
        {
            Debug.Log("colliding");


            AiAgent enemyAgent = other.GetComponentInParent<AiAgent>();
            EnemyHealth eh = other.GetComponent<EnemyHealth>();
            Debug.Log("executecollide");
            // && enemyAgent.stateMachine.currentState != AiStateId.StunnedState
            if (enemyAgent != null && eh.isEnemyHit == false)
            {
                StunnedState ss = other.GetComponent<StunnedState>();
                eh.TakeHammerDamage(hammerDamage);
                ss.selectedForceType = StunType.HammerKnockback;
                enemyAgent.stateMachine.ChangeState(AiStateId.StunnedState);
                audioSource.PlayOneShot(hitSoundEffects[Random.Range(0, 3)]);
                durability--;
            }

        }
    }

    // void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.collider.CompareTag("EnemyAttack"))
    //     {
    //         AiAgent enemyAgent = collision.collider.GetComponentInParent<AiAgent>();
    //         StunnedState ss = collision.collider.GetComponent<StunnedState>();
    //         // && enemyAgent.stateMachine.currentState != AiStateId.StunnedState
    //         if (enemyAgent != null)
    //         {
    //             ss.selectedForceType = StunType.HammerKnockback;
    //             enemyAgent.stateMachine.ChangeState(AiStateId.StunnedState);
    //             audioSource.PlayOneShot(bonk);
    //             durability--;
    //         }
    //     }
    // }


}

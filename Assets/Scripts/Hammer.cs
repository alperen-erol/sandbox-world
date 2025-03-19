using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    Animator animator;
    new BoxCollider collider;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip bonk;
    [SerializeField] TMP_Text durabilityText;
    [SerializeField] Animator camAnimator;
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
        HammerCooldownTimer -= Time.deltaTime;
        if (durability <= 0)
        {
            durabilityText.text = "Weapon Broken";
        }
        else
        {
            durabilityText.text = "Durability: " + durability;
        }


        if (Input.GetKeyDown(KeyCode.Mouse0) && HammerCooldownTimer <= Time.deltaTime)
        {
            animator.SetTrigger("Attack");
            camAnimator.SetTrigger("HammerSwing");
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAttack"))
        {
            Debug.Log("colliding");
            if (HammerCooldownTimer <= Time.deltaTime)
            {
                Debug.Log("executecollide");
                HammerCooldownTimer = HammerCooldown;
                AiAgent enemyAgent = other.GetComponentInParent<AiAgent>();
                StunnedState ss = other.GetComponent<StunnedState>();
                // && enemyAgent.stateMachine.currentState != AiStateId.StunnedState
                if (enemyAgent != null)
                {
                    ss.selectedForceType = StunType.HammerKnockback;
                    enemyAgent.stateMachine.ChangeState(AiStateId.StunnedState);
                    audioSource.PlayOneShot(bonk);
                    durability--;
                }
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

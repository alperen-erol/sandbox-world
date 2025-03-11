using UnityEngine;

public class Hammer : MonoBehaviour
{
    Animator animator;
    new BoxCollider collider;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip bonk;
    public float durability;

    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider>();
        collider.enabled = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Attack");
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("EnemyAttack"))
        {

            AiAgent enemyAgent = collision.collider.GetComponentInParent<AiAgent>();
            if (enemyAgent != null && enemyAgent.stateMachine.currentState != AiStateId.StunnedState)
            {
                enemyAgent.stateMachine.ChangeState(AiStateId.StunnedState);
                audioSource.PlayOneShot(bonk);
                durability--;
            }
        }
    }


}

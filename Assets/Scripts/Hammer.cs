using UnityEngine;

public class Hammer : MonoBehaviour
{
    Animator animator;
    new BoxCollider collider;

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
            if (enemyAgent != null)
            {
                enemyAgent.stateMachine.ChangeState(AiStateId.StunnedState);
            }
        }
    }


}

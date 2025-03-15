using System.Collections;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Sceptre : MonoBehaviour
{
    Animator anim;
    Collider col;
    [SerializeField] GameObject Holder;
    public bool weaponOnCooldown = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider>();
    }


    void Update()
    {
        HandleWeapon();
        // HandleThrowEnemy();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyAttack")
        {

            // StartCoroutine(Cooldown());
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            GameObject HeldEnemy = other.gameObject;
            NavMeshAgent aiAgent = HeldEnemy.GetComponent<NavMeshAgent>();
            aiAgent.enabled = false;
            other.transform.position = Holder.transform.position;
            rb.AddComponent<FixedJoint>().connectedBody = Holder.GetComponent<Rigidbody>();
            rb.isKinematic = false;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "EnemyAttack")
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            FixedJoint fj = other.GetComponent<FixedJoint>();
            Destroy(fj);
            rb.isKinematic = false;
            AiAgent agent = other.GetComponent<AiAgent>();
            StartCoroutine(Wait(agent));
        }
    }


    // void HandleThrowEnemy()
    // {
    //     if (isColliding == true && Input.GetKeyDown(KeyCode.Mouse1))
    //     {
    //         isColliding = false;
    //         Debug.Log("Enemy Throwed");
    //         FixedJoint fj = HeldEnemy.GetComponent<FixedJoint>();
    //         Destroy(fj);
    //         HeldEnemy = null;
    //         agent.stateMachine.ChangeState(AiStateId.StunnedState);
    //     }
    // }




    void HandleWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("Attack");
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            anim.SetTrigger("ReleaseAttack");
        }
    }

    public void EnableCollider()
    {
        col.enabled = true;
    }

    public void DisableCollider()
    {
        col.enabled = false;
    }

    IEnumerator Wait(AiAgent agent)
    {
        yield return new WaitForSeconds(0.03f);
        agent.stateMachine.ChangeState(AiStateId.StunnedState);
    }

    // IEnumerator Cooldown()
    // {
    //     weaponOnCooldown = true;
    //     yield return new WaitForSeconds(4f);
    //     weaponOnCooldown = false;
    // }
}

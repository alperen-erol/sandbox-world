using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSceptre : MonoBehaviour
{

    [SerializeField] Camera cam;
    [SerializeField] float maxGrabDistance = 10f, throwForce = 20f, lerpSpeed = 10f;
    [SerializeField] Transform objectHolder;
    Animator anim;

    Rigidbody grabbedRB;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (grabbedRB)
        {
            Vector3 movePos = objectHolder.position - grabbedRB.position;
            grabbedRB.AddForce(movePos * lerpSpeed);
            grabbedRB.linearDamping = 10f;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("Attack");
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if (Physics.Raycast(ray, out RaycastHit hit, maxGrabDistance))
            {
                if (hit.collider.gameObject.tag == "EnemyAttack")
                {
                    grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                    AiAgent aiAgent = hit.collider.gameObject.GetComponent<AiAgent>();
                    aiAgent.stateMachine.ChangeState(AiStateId.AirborneState);
                }
            }
        }


        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            anim.SetTrigger("ReleaseAttack");
            if (grabbedRB)
            {
                // grabbedRB.isKinematic = false;
                // grabbedRB.useGravity = true;
                AiAgent aiAgent = grabbedRB.GetComponent<AiAgent>();
                StunnedState ss = grabbedRB.GetComponent<StunnedState>();
                ss.selectedForceType = StunType.ScepterKnockback;
                aiAgent.stateMachine.ChangeState(AiStateId.StunnedState);
                grabbedRB = null;
            }
        }
    }
}
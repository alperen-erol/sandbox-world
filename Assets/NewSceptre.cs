using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewSceptre : MonoBehaviour
{

    [SerializeField] Camera cam;
    [SerializeField] float maxGrabDistance = 10f, throwForce = 20f, lerpSpeed = 10f;
    [SerializeField] Transform objectHolder;
    [SerializeField] TMP_Text energyText;
    public float energy, energyConsumeRate;
    public bool isEnergyConsume;
    Animator anim;

    Rigidbody grabbedRB;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (energy < 0)
            energy = 0;

        energyText.text = "Energy: " + Mathf.FloorToInt(energy);

        if (isEnergyConsume)
        {
            energy -= energyConsumeRate;
        }
        else
        {
            energy += energyConsumeRate / 2;
        }

        if (grabbedRB)
        {
            Vector3 movePos = objectHolder.position - grabbedRB.position;
            grabbedRB.AddForce(movePos * lerpSpeed);
            grabbedRB.linearDamping = 10f;
            if (energy <= 0)
            {

            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && energy > 0)
        {
            isEnergyConsume = true;
            anim.SetTrigger("Attack");
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if (Physics.Raycast(ray, out RaycastHit hit, maxGrabDistance))
            {
                if (hit.collider.gameObject.tag == "EnemyAttack" && energy > 0)
                {
                    grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                    AiAgent aiAgent = hit.collider.gameObject.GetComponent<AiAgent>();
                    aiAgent.stateMachine.ChangeState(AiStateId.AirborneState);
                }
            }
        }


        if (Input.GetKeyUp(KeyCode.Mouse0) || energy <= 0)
        {
            Debug.Log("mk up")
; isEnergyConsume = false;
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Sceptre_Release"))
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
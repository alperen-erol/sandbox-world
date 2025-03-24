using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

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
        energy = Mathf.Clamp(energy, 0, 100);

        energyText.text = "Energy: " + Mathf.FloorToInt(energy);

        HandleEnergy();

        if (grabbedRB)
        {
            Vector3 movePos = objectHolder.position - grabbedRB.position;
            grabbedRB.AddForce(movePos * lerpSpeed);
            grabbedRB.linearDamping = 10f;
            if (Input.GetKeyDown(KeyCode.Mouse1) && grabbedRB.gameObject.tag == "StonePiece")
            {
                grabbedRB.linearDamping = 0f;
                grabbedRB.AddForce(cam.transform.forward * throwForce, ForceMode.Impulse);
                grabbedRB = null;
            }

        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && energy > 0)
        {
            isEnergyConsume = true;
            anim.SetTrigger("Attack");
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit[] hits = Physics.RaycastAll(ray, maxGrabDistance);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.CompareTag("EnemyAttack") && energy > 0)
                {
                    grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                    AiAgent aiAgent = hit.collider.gameObject.GetComponent<AiAgent>();
                    aiAgent.stateMachine.ChangeState(AiStateId.AirborneState);
                }
                else if (hit.collider.gameObject.CompareTag("StonePiece") && energy > 0)
                {
                    grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                }
            }
            // if (Physics.RaycastAll(ray, out RaycastHit[] hits, maxGrabDistance))
            // {
            //     if (hit.collider.gameObject.tag == "EnemyAttack" && energy > 0)
            //     {
            //         grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
            //         AiAgent aiAgent = hit.collider.gameObject.GetComponent<AiAgent>();
            //         aiAgent.stateMachine.ChangeState(AiStateId.AirborneState);
            //     }
            //     else if (hit.collider.gameObject.tag == "StonePiece" && energy > 0)
            //     {
            //         grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
            //     }
            // }
        }


        if (Input.GetKeyUp(KeyCode.Mouse0) || energy <= 0)
        {
            Debug.Log("mk up or energy < 0");
            isEnergyConsume = false;
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Sceptre_Release"))
                anim.SetTrigger("ReleaseAttack");

            if (grabbedRB)
            {
                // grabbedRB.isKinematic = false;
                // grabbedRB.useGravity = true;
                if (grabbedRB.gameObject.tag == "StonePiece")
                {
                    grabbedRB.linearDamping = 0;
                    grabbedRB = null;
                }
                else
                {
                    AiAgent aiAgent = grabbedRB.GetComponent<AiAgent>();
                    StunnedState ss = grabbedRB.GetComponent<StunnedState>();
                    ss.selectedForceType = StunType.ScepterKnockback;
                    aiAgent.stateMachine.ChangeState(AiStateId.StunnedState);
                    grabbedRB = null;
                }
            }
        }
    }

    private void HandleEnergy()
    {
        if (isEnergyConsume)
        {
            energy -= energyConsumeRate;
        }
        else
        {
            energy += energyConsumeRate / 2;
        }
    }
}
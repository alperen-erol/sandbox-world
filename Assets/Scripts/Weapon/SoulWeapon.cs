using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SoulWeapon : MonoBehaviour
{
    [SerializeField] GameObject SoulProjectile;
    [SerializeField] float gatheredSoulAmount;
    [SerializeField] float gatherSoulRate;
    [SerializeField] float soulThrowForce;
    [SerializeField] float soulDistance;
    [SerializeField] float shootCost;

    [SerializeField] Light weaponLight;

    [SerializeField] ParticleSystem SoulGatherParticles;
    private ParticleSystem.EmissionModule emission;
    private ParticleSystem.MainModule main;
    private ParticleSystem.ShapeModule shape;

    [SerializeField] private float emissionRate = 100f;
    [SerializeField] private float particleSpeed = 5f;
    [SerializeField] private float particleSize = 0.2f;

    public LineRenderer lineRenderer;
    public Transform weaponTip;
    public GameObject grabbedRB;

    bool updateLineRenderer = false, isEnemyPicked = false, isGatherSouls = false;

    Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
        emission = SoulGatherParticles.emission;
        main = SoulGatherParticles.main;
        shape = SoulGatherParticles.shape;
    }

    RaycastHit hit;
    EnemyHealth health;
    void Update()
    {
        weaponLight.intensity = gatheredSoulAmount / 100;
        emission.rateOverTime = gatheredSoulAmount * 5;
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out hit, soulDistance);
            if (hit.collider.CompareTag("EnemyAttack"))
            {
                if (!grabbedRB)
                    grabbedRB = hit.collider.gameObject;
                isEnemyPicked = true;
                isGatherSouls = true;

                // StartParticleEmission();

                health = grabbedRB.gameObject.GetComponent<EnemyHealth>();
                health.isDrain = true;
                health.gatherSoulRate = gatherSoulRate;
                health.DrainEnemyHealth(gatherSoulRate);

                // lineRenderer.enabled = true;
                // updateLineRenderer = true;

                animator.SetBool("SoulDrain", true);
                health.StartSoulDrain();
                StartSoulGather();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1) && grabbedRB || !grabbedRB && isEnemyPicked)
        {
            StopDraining();
            // StopParticleEmission();
        }


        if (isGatherSouls)
        {
            StartSoulGather();
            // UpdateParticleSystem();
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireSoul();
        }


        // if (updateLineRenderer && grabbedRB)
        // {
        //     lineRenderer.SetPosition(0, weaponTip.position);
        //     lineRenderer.SetPosition(1, grabbedRB.transform.position);
        // }
    }


    private void StopDraining()
    {
        Debug.Log("stoppedDraining");
        isEnemyPicked = false;
        isGatherSouls = false;

        // lineRenderer.enabled = false;
        // updateLineRenderer = false;

        if (grabbedRB)
        {
            health.isDrain = false;
            health.StopSoulDrain();
        }
        animator.SetBool("SoulDrain", false);
        Debug.Log("souldrainfalse");
        grabbedRB = null;
        health = null;
    }

    private void StartSoulGather()
    {
        gatheredSoulAmount += Time.deltaTime * gatherSoulRate;
    }


    private void FireSoul()
    {
        if (gatheredSoulAmount >= shootCost)
        {
            gatheredSoulAmount -= shootCost;
            InstantiateSoulProjectile();
            animator.SetTrigger("Shoot");
        }
        else
            animator.SetTrigger("Empty");
    }


    private void InstantiateSoulProjectile()
    {
        GameObject projectile = Instantiate(SoulProjectile, weaponTip.position, Camera.main.transform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Camera.main.transform.forward * soulThrowForce;
        }
    }


    // private void StartParticleEmission()
    // {
    //     emission.enabled = true;
    // }


    // private void StopParticleEmission()
    // {
    //     emission.enabled = false;
    // }

    // private void UpdateParticleSystem()
    // {
    //     Vector3 direction = weaponTip.position - grabbedRB.transform.position;
    //     SoulGatherParticles.transform.position = grabbedRB.transform.position;
    //     SoulGatherParticles.transform.rotation = Quaternion.LookRotation(direction);
    // }
}

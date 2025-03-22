using System;
using Unity.Mathematics;
using UnityEngine;

public class SoulWeapon : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform weaponTip;
    public Transform enemyTarget;
    public ParticleSystem soulDrainParticles;
    [SerializeField] GameObject SoulProjectile;
    [SerializeField] float gatheredSoulAmount;
    [SerializeField] float gatherSoulRate;
    [SerializeField] float soulThrowForce;
    Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, weaponTip.position);
            lineRenderer.SetPosition(1, enemyTarget.position);
            enemyTarget.gameObject.GetComponent<EnemyHealth>().isDrain = true;
            animator.SetBool("SoulDrain", true);
            StartSoulDrainEffect();
            StartSoulGather();
        }
        else
        {
            lineRenderer.enabled = false;
            StopDrainEffect();
            enemyTarget.gameObject.GetComponent<EnemyHealth>().isDrain = false;
            animator.SetBool("SoulDrain", false);
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireSoul();
        }
    }


    private void StartSoulGather()
    {
        gatheredSoulAmount += Time.deltaTime * gatherSoulRate;
    }


    private void FireSoul()
    {
        if (gatheredSoulAmount >= 5)
        {
            gatheredSoulAmount -= 5;
            InstantiateSoulProjectile();
        }
    }


    private void InstantiateSoulProjectile()
    {
        GameObject projectile = Instantiate(SoulProjectile, weaponTip.position, Camera.main.transform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Camera.main.transform.forward * soulThrowForce; // Adjust speed as needed
        }
    }


    void StartSoulDrainEffect()
    {
        if (!soulDrainParticles.isPlaying)
            soulDrainParticles.Play();
    }


    void StopDrainEffect()
    {
        soulDrainParticles.Stop();
    }
}

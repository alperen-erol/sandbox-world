using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyWeapon1 : MonoBehaviour
{
    BoxCollider boxCollider;
    [SerializeField] float knocbackForce = 10f;
    [SerializeField] float knocbackForceY = 10f;


    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

    }


    void Update()
    {

    }


    [SerializeField] private float damage = 20f;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("player hit");
            GameObject player = other.gameObject;
            if (player != null)
            {
                player.GetComponent<PlayerHealth>().TakeDamage(damage);
                Rigidbody playerrb = other.GetComponent<Rigidbody>();
                Vector3 knockbackDirection = (playerrb.transform.position - this.transform.position).normalized;
                playerrb.linearVelocity = Vector3.zero;
                playerrb.AddForce(knockbackDirection * knocbackForce, ForceMode.Impulse);
                playerrb.AddForce(Vector3.up * knocbackForceY, ForceMode.Impulse);
                StartCoroutine(HitCooldown());
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {

    }


    IEnumerator HitCooldown()
    {
        boxCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        boxCollider.enabled = true;
    }
}

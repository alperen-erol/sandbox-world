using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyWeapon1 : MonoBehaviour
{
    BoxCollider boxCollider;
    [SerializeField] Transform enemyTransform;
    [SerializeField] Transform playerTransform;
    Vector3 knockbackDirection;
    [SerializeField] float knocbackForce = 10f;
    [SerializeField] float knocbackForceY = 10f;


    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        knockbackDirection = (playerTransform.position - enemyTransform.position).normalized;
    }


    void Update()
    {

    }


    [SerializeField] private float damage = 20f;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("player hit");
            GameObject player = collision.collider.gameObject;
            if (player != null)
            {
                player.GetComponent<PlayerHealth>().TakeDamage(damage);
                StartCoroutine(HitCooldown());
                Rigidbody playerrb = player.GetComponent<Rigidbody>();
                playerrb.AddForce(knockbackDirection * knocbackForce, ForceMode.Impulse);
                playerrb.AddForce(Vector3.up * knocbackForceY, ForceMode.Impulse);
            }
        }
    }


    IEnumerator HitCooldown()
    {
        boxCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        boxCollider.enabled = true;
    }
}

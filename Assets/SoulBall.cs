using UnityEngine;

public class SoulBall : MonoBehaviour
{
    [SerializeField] float radius = 5.0F;
    [SerializeField] float power = 10.0F;


    void OnCollisionEnter(Collision collision)
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            if(hit.tag=="EnemyAttack")
            {
                
            }
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }
        Destroy(this.gameObject);
    }


}

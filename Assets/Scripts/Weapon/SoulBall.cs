using UnityEngine;

public class SoulBall : MonoBehaviour
{
    [SerializeField] float radius = 5.0F;
    [SerializeField] float power = 10.0F;
    [SerializeField] ParticleSystem BallImpactExpllosion;

    void OnCollisionEnter(Collision collision)
    {
        Instantiate(BallImpactExpllosion, transform.position, Quaternion.identity);
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            if (hit.tag == "EnemyAttack")
            {
                AiAgent agent = hit.GetComponent<AiAgent>();
                StunnedState ss = hit.GetComponent<StunnedState>();
                EnemyHealth health = hit.GetComponent<EnemyHealth>();
                health.enemyHealth -= 20;
                ss.selectedForceType = StunType.SoulKnockback;
                ss.ballPos = this.transform.position;
                agent.stateMachine.ChangeState(AiStateId.StunnedState);
            }
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }
        Destroy(this.gameObject);
    }


}

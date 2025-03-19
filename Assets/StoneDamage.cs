using UnityEngine;

public class StoneDamage : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "EnemyAttack")
        {
            AiAgent aiAgent = other.collider.gameObject.GetComponent<AiAgent>();
            StunnedState ss = other.gameObject.GetComponent<StunnedState>();
            ss.selectedForceType = StunType.StoneKnockBack;
            aiAgent.stateMachine.ChangeState(AiStateId.StunnedState);
            Destroy(this.gameObject);
        }
    }
}

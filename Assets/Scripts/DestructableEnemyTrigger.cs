using System.Collections.Generic;
using UnityEngine;

public class DestructableEnemyTrigger : MonoBehaviour
{

    private List<Enemy> capturedEnemies = new List<Enemy>();
    public bool objectDestroyed = false;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyAttack")
        {
            capturedEnemies.Add(other.GetComponent<Enemy>());
        }
    }

    public void ObjectDestroyed()
    {
        foreach (Enemy enemy in capturedEnemies)
        {
            GameObject enemyObject = enemy.gameObject;
            StunnedState ss = enemyObject.GetComponent<StunnedState>();
            ss.selectedForceType = StunType.ObstacleDestruction ;
            enemyObject.GetComponent<AiAgent>().stateMachine.ChangeState(AiStateId.StunnedState);
        }
    }
}

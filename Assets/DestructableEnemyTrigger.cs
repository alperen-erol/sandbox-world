using System.Collections.Generic;
using UnityEngine;

public class DestructableEnemyTrigger : MonoBehaviour
{

    private List<Enemy> capturedEnemies = new List<Enemy>();
    public bool objectDestroyed = false;


    // bool x = false;
    // void Update()
    // {
    //     if (!x && objectDestroyed)
    //     {
    //         ObjectDestroyed();
    //         x = true;
    //     }
    // }


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
            enemyObject.GetComponent<AiAgent>().stateMachine.ChangeState(AiStateId.StunnedState);
        }
    }
}

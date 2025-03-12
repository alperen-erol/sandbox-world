using UnityEngine;

public class PrisonCell : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyAttack")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && !enemy.isCaptured)
            {
                enemy.isCaptured = true;
                PrisonManager.Instance.EnemyCaptured(enemy);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.isCaptured = false;
            PrisonManager.Instance.EnemyReleased(enemy);
        }
    }

}
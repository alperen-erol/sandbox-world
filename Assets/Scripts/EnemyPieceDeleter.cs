using System.Collections;
using UnityEngine;

public class EnemyPieceDeleter : MonoBehaviour
{
    float deleteDelay = 5f;
    private void Start()
    {
        StartCoroutine(Delete());
    }

    private IEnumerator Delete()
    {
        yield return new WaitForSeconds(deleteDelay);
        Destroy(this.gameObject);
    }
}

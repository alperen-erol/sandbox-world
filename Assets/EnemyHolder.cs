using UnityEngine;

public class EnemyHolder : MonoBehaviour
{

    [SerializeField] GameObject player;


    void Update()
    {
        this.gameObject.transform.position = player.transform.position + new Vector3(0, 2f, 5f);
    }
}

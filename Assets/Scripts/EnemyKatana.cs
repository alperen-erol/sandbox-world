using UnityEngine;

public class EnemyKatana : MonoBehaviour
{
    public EnemyAi ea;

    private void Start()
    {
        ea = GetComponentInParent<EnemyAi>();
    }

    public void HandleParry()
    {
        ea.HandleParry();
    }
}

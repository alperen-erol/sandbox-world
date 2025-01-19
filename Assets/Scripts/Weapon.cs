using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Animator animator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Attack");
        }
    }
}
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class Parry : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] parrySounds;
    private Collider collider;

    public WeaponAnim wa;

    private void Start()
    {
        collider = GetComponent<Collider>();
        collider.enabled = false;
    }

    private void Update()
    {
        CheckBlock();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (wa.isBlocking)
        {
            Debug.Log("parry");
            other.gameObject.GetComponent<EnemyKatana>().HandleParry();
            HandleParry();
        }
    }

    public void HandleParry()
    {
        audioSource.PlayOneShot(parrySounds[Random.Range(0, parrySounds.Length)]);
    }

    public void CheckBlock()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
            collider.enabled = true;
        else if (Input.GetKeyUp(KeyCode.Mouse1))
            collider.enabled = false;
    }
}

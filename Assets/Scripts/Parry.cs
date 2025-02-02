using UnityEngine;
using UnityEngine.Audio;

public class Parry : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] parrySounds;

    public WeaponAnim wa;

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
}

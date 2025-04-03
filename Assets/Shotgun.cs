using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] private int pelletCount = 8;        // Number of pellets/raycasts
    [SerializeField] private float maxSpreadAngle = 20f; // Maximum spread angle in degrees
    [SerializeField] private float range = 50f;          // Maximum raycast distance
    [SerializeField] private float damage = 10f;         // Damage per pellet
    [SerializeField] LayerMask Enemy;
    [SerializeField] Transform gunTip;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip cock;
    [SerializeField] AudioClip cock2;
    [SerializeField] AudioClip shoot;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayCock()
    {
        audioSource.PlayOneShot(cock);
    }

    public void PlayCock2()
    {
        audioSource.PlayOneShot(cock2);
    }

    public void PlayShoot()
    {
        audioSource.PlayOneShot(shoot);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("Shoot");
        }
    }

    public void ShotgunFired()
    {
        FireShotgun(transform.forward);
    }


    public void FireShotgun(Vector3 direction)
    {
        Debug.Log("firing shotung");
        // Ensure direction is normalized
        direction = direction.normalized;

        for (int i = 0; i < pelletCount; i++)
        {
            // Generate random angle within the cone
            float randomAngleX = Random.Range(-maxSpreadAngle, maxSpreadAngle);
            float randomAngleY = Random.Range(-maxSpreadAngle, maxSpreadAngle);

            Quaternion randomRotation = Quaternion.Euler(randomAngleX, randomAngleY, 0);
            Vector3 spreadDirection = randomRotation * direction;
            RaycastHit hit;
            if (Physics.Raycast(gunTip.position, spreadDirection, out hit, range, Enemy))
            {
                Debug.DrawRay(gunTip.position, spreadDirection * hit.distance, Color.red, 1f);
                EnemyHealth eh = hit.collider.GetComponent<EnemyHealth>();
                eh.TakeDamage(damage);
                // hit pointte particle yap
            }
            else
            {
                Debug.DrawRay(gunTip.position, spreadDirection * range, Color.green, 1f);
            }
        }
    }
}


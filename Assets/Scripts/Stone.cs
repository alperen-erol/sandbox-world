using Unity.VisualScripting;
using UnityEngine;

public class Stone : MonoBehaviour
{
    Inventory im;
    Rigidbody rb;
    Animator anim;

    [SerializeField] float throwPower = 10f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        im = FindFirstObjectByType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            HandleThrow();
        }
        if (im.destroyBallInHand)
        {
            Debug.Log("Destroying ball in hand");
            Destroy(this.gameObject);
        }
    }
    private void HandleThrow()
    {
        anim.enabled = false;
        this.gameObject.transform.parent = null;
        rb.isKinematic = false;
        rb.AddForce(this.gameObject.transform.forward * throwPower, ForceMode.Force);
        im.playerHasBalls = false;
        if (im.stoneCount > 0)
        {
            im.stoneCount--;
            im.InstantiateBall();
        }
        this.enabled = false;
    }
}

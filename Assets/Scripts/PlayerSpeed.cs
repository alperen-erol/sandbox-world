using TMPro;
using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
    [SerializeField] TMP_Text speedText;
    [SerializeField] Rigidbody rb;

    public float speed;

    private void FixedUpdate()
    {
        // Calculate horizontal velocity
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        speed = horizontalVelocity.magnitude;

        // Display the speed
        speedText.text = speed.ToString("F2");
    }

    public Vector3 ReturnVelocity()
    {
        return new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
    }


}
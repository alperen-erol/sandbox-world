using TMPro;
using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
    [SerializeField] TMP_Text speedText;
    [SerializeField] Rigidbody rb;

    float speed;

    private void FixedUpdate()
    {
        speed = rb.linearVelocity.magnitude;

        speedText.text = speed.ToString("F2");
    }
}

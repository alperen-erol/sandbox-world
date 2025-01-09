using Unity.Mathematics;
using UnityEngine;

public class PlayerOrientation : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] Rigidbody rb;
    float yRotation;
    Quaternion targetRotation;

    private void Update()
    {
        yRotation = cameraTransform.eulerAngles.y;
        targetRotation = Quaternion.Euler(0f, yRotation, 0f);
        rb.MoveRotation(targetRotation);
        Debug.Log("player rotation" + targetRotation);
    }
}
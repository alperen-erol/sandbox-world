using UnityEngine;

public class PlayerOrientation : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] Rigidbody rb;

    private void LateUpdate()
    {
        float yRotation = cameraTransform.eulerAngles.y;
        Quaternion targetRotation = Quaternion.Euler(0f, yRotation, 0f);
        rb.MoveRotation(targetRotation);
        Debug.Log("player rotation" + targetRotation);
    }
}
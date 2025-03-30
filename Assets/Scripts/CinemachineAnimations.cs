using Unity.Cinemachine;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    CinemachineCamera cinemachineCam;
    [SerializeField] float tiltAmount = 5f;
    [SerializeField] float mouseTiltAmount = 5f;
    [SerializeField] float tiltSpeed = 5f;
    [SerializeField] float currentTilt = 0f;

    [SerializeField] float tiltLimit;
    float lookInput, targetTilt, input;
    void Start()
    {
        cinemachineCam = GetComponent<CinemachineCamera>();
    }

    void Update()
    {
        lookInput = Input.GetAxis("Mouse X");
        input = Input.GetAxis("Horizontal");
        targetTilt = -input * tiltAmount;
        targetTilt += -lookInput * mouseTiltAmount;
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltSpeed);
        currentTilt = Mathf.Clamp(currentTilt, -tiltLimit, tiltLimit);
        cinemachineCam.Lens.Dutch = currentTilt;
    }
}

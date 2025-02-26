using Unity.Cinemachine;
using UnityEngine;

public class CameraSpeedFov : MonoBehaviour
{
    [SerializeField] CinemachineCamera cam;

    PlayerSpeed ps;

    public float baseFOV = 60f;
    public float lerpSpeed = 2f;
    public float finalFov;

    private void Start()
    {
        ps = GetComponent<PlayerSpeed>();
        finalFov = cam.Lens.FieldOfView;
        cam.Lens.FieldOfView = baseFOV + ps.speed;
    }

    private void Update()
    {
        CalculateFOV();
    }

    private void CalculateFOV()
    {
        float targetFOV = baseFOV + ps.speed;
        cam.Lens.FieldOfView = Mathf.Lerp(cam.Lens.FieldOfView, targetFOV, lerpSpeed * Time.deltaTime);
        finalFov = cam.Lens.FieldOfView;
    }
}
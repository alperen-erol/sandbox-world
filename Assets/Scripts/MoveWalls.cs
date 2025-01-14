using UnityEngine;

public class MoveWalls : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject Wall1;
    [SerializeField] GameObject Wall2;
    [SerializeField] ObjectInteract oi;

    [Header("Movement Settings")]
    [SerializeField] float wallMoveDistance = 2.5f;
    [SerializeField] float wallMoveSpeed = 1f;

    private Vector3 wall1StartPosition;
    private Vector3 wall2StartPosition;
    private Vector3 wall1TargetPosition;
    private Vector3 wall2TargetPosition;
    private float lerpProgress = 0f;
    public bool moveDone;

    private void Start()
    {
        wall1StartPosition = Wall1.transform.position;
        wall2StartPosition = Wall2.transform.position;
        wall1TargetPosition = wall1StartPosition + new Vector3(-wallMoveDistance, 0, 0);
        wall2TargetPosition = wall2StartPosition + new Vector3(wallMoveDistance, 0, 0);
        moveDone = false;
    }

    private void Update()
    {
        if (oi.loadDone)
        {
            HandleWalls();
            moveDone = true;
        }
    }

    private void HandleWalls()
    {
        lerpProgress += wallMoveSpeed * Time.deltaTime;
        Debug.Log(lerpProgress);
        float smoothProgress = Mathf.SmoothStep(0f, 1f, lerpProgress);
        Wall1.transform.position = Vector3.Lerp(wall1StartPosition, wall1TargetPosition, smoothProgress);
        Wall2.transform.position = Vector3.Lerp(wall2StartPosition, wall2TargetPosition, smoothProgress);

        if (lerpProgress >= 1f)
        {
            oi.loadDone = false;
        }
    }

}
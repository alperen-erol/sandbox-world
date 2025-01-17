using UnityEditor.Rendering;
using UnityEngine;

public class Swinging : MonoBehaviour
{
    [Header("Input")]
    public KeyCode swingKey = KeyCode.Mouse0;

    [Header("References")]
    public LineRenderer lr;
    public Transform gunTip, cam, player;
    public LayerMask whatIsGrappleable;
    public PlayerMovementAdvanced pm;
    private SoundSystem ss;

    [Header("Swinging")]
    public float maxSwingDistance = 25f;
    private Vector3 swingPoint;
    private SpringJoint joint;

    [Header("Joint Settings")]
    public float spring = 4.5f;
    public float damper = 7f;
    public float massScale = 4.5f;



    private void Start()
    {
        pm = GetComponent<PlayerMovementAdvanced>();
        ss = GetComponent<SoundSystem>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(swingKey)) StartSwing();
        if (Input.GetKeyUp(swingKey)) StopSwing();
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    private void StartSwing()
    {
        pm.swinging = true;

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxSwingDistance, whatIsGrappleable))
        {
            ss.PlayGrapplingHookLand(hit.transform);
            swingPoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;

            float distanceFromPoint = Vector3.Distance(player.position, swingPoint);

            /* joint.maxDistance = distanceFromPoint * 0.8f; */
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = spring;
            joint.damper = damper;
            joint.massScale = massScale;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
            lr.enabled = true;
        }
    }
    void StopSwing()
    {
        pm.swinging = false;
        lr.positionCount = 0;
        Destroy(joint);
    }

    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 8f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, swingPoint);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [Header("Wallrunning")]
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float wallRunForce = 200f;
    public float wallJumpUpForce = 7f;
    public float wallJumpSideForce = 12f;
    public float wallClimbSpeed = 3f;
    public float maxWallRunTime = 1.5f;
    private float wallRunTimer;

    [Header("Input")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode upwardsRunKey = KeyCode.LeftShift;
    public KeyCode downwardsRunKey = KeyCode.LeftControl;
    private bool upwardsRunning;
    private bool downwardsRunning;
    private float horizontalInput;
    private float verticalInput;

    [Header("Detection")]
    public float wallCheckDistance = 0.7f;
    public float minJumpHeight = 1.5f;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private bool wallLeft;
    private bool wallRight;

    [Header("Exiting")]
    private bool exitingWall;
    public float exitWallTime = 0.2f;
    private float exitWallTimer;

    [Header("Momentum")]
    public float momentumDecayRate = 2f;
    public float initialMomentumMultiplier = 0.8f;
    public bool preserveMomentum = true;
    private float currentMomentum;

    [Header("Wall Bump Prevention")]
    public float wallBumpForce = 10f;
    public float minWallRunVelocity = 1f;
    private bool isWallBumping = false;
    private float wallBumpCooldown = 0.5f;
    private float wallBumpTimer = 0f;

    [Header("References")]
    public Transform orientation;
    private PlayerMovementAdvanced pm;
    private Rigidbody rb;
    private SoundSystem ss;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovementAdvanced>();
        ss = GetComponent<SoundSystem>();
    }

    private void Update()
    {
        CheckForWall();
        StateMachine();

        if (isWallBumping)
        {
            wallBumpTimer -= Time.deltaTime;
            if (wallBumpTimer <= 0)
            {
                isWallBumping = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (pm.wallrunning)
            WallRunningMovement();
        else if (ShouldHandleWallBump())
            HandleWallBump();
    }

    private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall);
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    private void StateMachine()
    {

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        upwardsRunning = Input.GetKey(upwardsRunKey);
        downwardsRunning = Input.GetKey(downwardsRunKey);

        if ((wallLeft || wallRight) && verticalInput > 0 && AboveGround() && !exitingWall)
        {
            if (!pm.wallrunning)
                StartWallRun();

            if (wallRunTimer > 0)
                wallRunTimer -= Time.deltaTime;

            if (wallRunTimer <= 0 && pm.wallrunning)
            {
                exitingWall = true;
                exitWallTimer = exitWallTime;
            }

            if (Input.GetKeyDown(jumpKey)) WallJump();
        }

        else if (exitingWall)
        {
            if (pm.wallrunning)
                StopWallRun();

            if (exitWallTimer > 0)
                exitWallTimer -= Time.deltaTime;

            if (exitWallTimer <= 0)
                exitingWall = false;
        }

        else
        {
            if (pm.wallrunning)
                StopWallRun();
        }
    }

    private bool ShouldHandleWallBump()
    {
        if (!pm.wallrunning && !exitingWall && (wallLeft || wallRight) && !pm.grounded && !isWallBumping)
        {
            Vector3 currentVelocity = rb.linearVelocity;
            float forwardSpeed = Vector3.Dot(currentVelocity, orientation.forward);

            if (forwardSpeed < minWallRunVelocity && forwardSpeed > 0)
            {
                return true;
            }
        }
        return false;
    }

    private void HandleWallBump()
    {
        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

        rb.AddForce(wallNormal * wallBumpForce, ForceMode.Impulse);

        isWallBumping = true;
        wallBumpTimer = wallBumpCooldown;
    }

    private void StartWallRun()
    {
        pm.wallrunning = true;
        wallRunTimer = maxWallRunTime;

        if (preserveMomentum)
        {
            currentMomentum = rb.linearVelocity.y * initialMomentumMultiplier;
        }
        else
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            currentMomentum = 0f;
        }
    }

    private void WallRunningMovement()
    {
        ss.PlayFootstep();
        rb.useGravity = false;

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
            wallForward = -wallForward;

        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

        float verticalVelocity = 0;

        if (upwardsRunning)
        {
            verticalVelocity = wallClimbSpeed;
            currentMomentum = 0;
        }
        else if (downwardsRunning)
        {
            verticalVelocity = -wallClimbSpeed;
            currentMomentum = 0;
        }
        else if (preserveMomentum && Mathf.Abs(currentMomentum) > 0.1f)
        {
            verticalVelocity = currentMomentum;

            if (currentMomentum > 0)
            {
                currentMomentum -= momentumDecayRate * Time.fixedDeltaTime;
                if (currentMomentum < 0) currentMomentum = 0;
            }
            else
            {
                currentMomentum += momentumDecayRate * Time.fixedDeltaTime;
                if (currentMomentum > 0) currentMomentum = 0;
            }
        }
        else
        {
            verticalVelocity = -0.5f;
        }

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, verticalVelocity, rb.linearVelocity.z);


        if (!(wallLeft && horizontalInput > 0) && !(wallRight && horizontalInput < 0))
            rb.AddForce(-wallNormal * 80f, ForceMode.Force);
    }

    private void StopWallRun()
    {
        pm.wallrunning = false;

        if (wallLeft || wallRight)
        {
            Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;
            rb.AddForce(wallNormal * 2f, ForceMode.Impulse);
        }

        if (preserveMomentum && currentMomentum > 0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, currentMomentum * 0.5f, rb.linearVelocity.z);
        }
    }

    private void WallJump()
    {
        ss.PlayJump();
        exitingWall = true;
        exitWallTimer = exitWallTime;

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

        Vector3 forceToApply = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);

        currentMomentum = 0;
    }
}
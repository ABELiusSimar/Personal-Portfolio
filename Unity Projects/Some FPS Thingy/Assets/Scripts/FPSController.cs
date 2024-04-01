using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class FPSController : MonoBehaviour
{
    [Header("Speed Variables")]
    public float WalkingSpeed = 7.5f;
    public float RunningSpeed = 11.5f;
    public float JumpSpeed = 8.0f;
    public float Gravity = 20.0f;
    public float LookSpeed = 2.0f;
    public float LookXLimit = 45.0f;
    public float DashSpeed;
    public float DashTime;
    public bool IsDashing = false;
    public float Stamina;
    public float MaxStamina;
    private float _RegenStamDelay = 2.0f;

    [Header("Camera")]
    public Camera PlayerCamera;
    CharacterController CharacterController;
    Vector3 MoveDirection = Vector3.zero;
    float RotationX = 0;
    public GameObject LH, RH;

    [Header("Shield")]
    public GameObject Shield;
    public bool IsBlocking = false;

    [Header("Stamina")]
    public HealthAndStaminaBarManager StaminaBar;

    [HideInInspector]
    public bool CanMove = true;

    void Start()
    {
        CharacterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Turn off shield
        Shield.SetActive(false);

        // Save max stamina for regen purposes
        MaxStamina = Stamina;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        // Less compicated form for CurSpeedX
        float curSpeedX = 0;
        if (CanMove == true)
        {
            if (isRunning == true)
            {
                curSpeedX = RunningSpeed * Input.GetAxis("Vertical");
            }
            else
            {
                curSpeedX = WalkingSpeed * Input.GetAxis("Vertical");
            }
        }
        else
        {
            //Do Nothing
        }

        // Less compicated form for CurSpeedY
        float curSpeedY = 0;
        if (CanMove == true)
        {
            if (isRunning == true)
            {
                curSpeedY = RunningSpeed * Input.GetAxis("Horizontal");
            }
            else
            {
                curSpeedY = WalkingSpeed * Input.GetAxis("Horizontal");
            }
        }
        else
        {
            //Do Nothing
        }

        float movementDirectionY = MoveDirection.y;
        MoveDirection = (forward * curSpeedX) + (right * curSpeedY);

        StaminaBar.UpdateStaminabar(MaxStamina, Stamina);

        // Determine jump, dash, and blocking (All actions use stamina)
        if (Stamina > 0)
        {
            // Stamina has regen completely, set the regen delay back to original value
            _RegenStamDelay = 2;

            if (Input.GetKeyDown(KeyCode.Space) && CanMove && CharacterController.isGrounded && Stamina > 10f)
            {
                MoveDirection.y = JumpSpeed;
                Stamina -= 25f;
            }
            else if (Input.GetKeyDown(KeyCode.LeftControl) && CanMove && Stamina > 10f)
            {
                // Set dashing to true
                IsDashing = true;
                StartCoroutine(Dash());
                Stamina -= 10f;
            }
            else if (Input.GetMouseButtonDown(1) && Stamina > 10f)
            {
                // Take less damage
                IsBlocking = true;
                // Instantiate a shield-like object
                Shield.SetActive(true);
            }
            else
            {
                MoveDirection.y = movementDirectionY;

                // Set is blocking to false
                if (Input.GetMouseButtonUp(1))
                {
                    IsBlocking = false;
                    Shield.SetActive(false);
                }
            }

            // Passively Regen Stamina
            RegenStamina(MaxStamina);

            // Player not dashing
            IsDashing = false;
        }
        else
        {
            // Reset value here as well to prevent incosistent buffering
            MoveDirection.y = movementDirectionY;
            // Stamina is 0
            Stamina = 0;
            // Delay 2 seconds before regen starts
            _RegenStamDelay -= Time.deltaTime;
            if (_RegenStamDelay <= 0)
            {
                RegenStamina(MaxStamina);
            }
        }

        // Dash Coroutine
        IEnumerator Dash()
        {
            float startTime = Time.time;

            while (Time.time < startTime + DashTime)
            {
                CharacterController.Move(MoveDirection * DashSpeed * Time.deltaTime);
                yield return null;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!CharacterController.isGrounded)
        {
            MoveDirection.y -= Gravity * Time.deltaTime;
        }

        // Move the controller
        CharacterController.Move(MoveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (CanMove)
        {
            RotationX += -Input.GetAxis("Mouse Y") * LookSpeed;
            RotationX = Mathf.Clamp(RotationX, -LookXLimit, LookXLimit);
            PlayerCamera.transform.localRotation = Quaternion.Euler(RotationX, 0, 0);
            LH.transform.localRotation = PlayerCamera.transform.localRotation;
            RH.transform.localRotation = PlayerCamera.transform.localRotation;
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * LookSpeed, 0);
        }
    }

    // Stamina Regen Function
    private void RegenStamina(float MaxStam)
    {
        if (Stamina < MaxStam)
        {
            Stamina += Time.deltaTime * 5;
        }
    }
}

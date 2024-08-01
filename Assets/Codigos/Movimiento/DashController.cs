using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class DashController : MonoBehaviour
{
    public float dashSpeed = 20f;
    public float dashDuration = 1.5f;

    private NetworkCharacterControllerCustom characterController;
    private Vector3 dashDirection;
    private float dashTime;

    void Start()
    {
        characterController = GetComponent<NetworkCharacterControllerCustom>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTime <= 0)
        {
            StartDash();
        }
    }

    void FixedUpdate()
    {
        if (dashTime > 0)
        {
            Dash();
        }
    }

    void StartDash()
    {
        dashDirection = transform.forward;
        dashTime = dashDuration;
        characterController.CanMove = false;
    }

    void Dash()
    {
        characterController.Controller.Move(dashDirection * dashSpeed * Time.fixedDeltaTime);
        dashTime -= Time.fixedDeltaTime;

        if (dashTime <= 0)
        {
            characterController.CanMove = true;
        }
    }
}

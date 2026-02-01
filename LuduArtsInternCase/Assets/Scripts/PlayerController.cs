using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction moveAction;
    InputAction cameraAction;
    InputAction interactionAction;

    [Header("Movement Settings")]
    public float walkSpeed = 5;

    [Header("Camera Settings")]
    public Camera camera;
    public float lookSensivity = 5;
    public float cameraClamp = 70;

    [Header("Interaction Settings")]
    public float interactDistance = 300;
    float inputHoldTime;
    bool holdingKey;



    float verticalRotation;



    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Movement");
        cameraAction = playerInput.actions.FindAction("Camera");
        interactionAction = playerInput.actions.FindAction("Interaction");
        interactionAction.started += OnInteractionStarted;
        interactionAction.canceled += OnInteractionCanceled;
    }


    private void Update()
    {
        MovePlayer();
        CameraMovement();
        if (holdingKey)
        {
            Interaction();
        }
    }


    void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        Vector3 move = transform.forward * direction.y + transform.right * direction.x;

        transform.position += move * Time.deltaTime * walkSpeed;
    }

    void CameraMovement()
    {
        Vector2 lookDirection = cameraAction.ReadValue<Vector2>();
      
        verticalRotation -= lookDirection.y * lookSensivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -cameraClamp, cameraClamp); 
        camera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);


        transform.Rotate(0, lookDirection.x * lookSensivity, 0);
        camera.transform.Rotate(lookDirection.y * lookSensivity, 0, 0);
    }


    void Interaction()
    {
        //Debug.Log("Interaction Ray");
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            if (hit.collider.TryGetComponent<I_Interaction>(out var interactable))
            {
                if (interactable != null)
                {
                    if (interactable.IsInteractable)
                    {
                        if (interactable.HoldInteract)
                        {
                            inputHoldTime += Time.deltaTime;
                            if (inputHoldTime >= interactable.HoldDuration)
                            {
                                holdingKey = false;
                                inputHoldTime = 0;
                                interactable.OnInteract();
                            }
                        }
                        else
                        {
                            holdingKey = false;
                            inputHoldTime = 0;
                            interactable.OnInteract();
                        }
                    }
                }
                else
                {
                    holdingKey = false;
                    inputHoldTime = 0;
                }
            }
        }
    }


    void OnInteractionStarted(InputAction.CallbackContext ctx)
    {
        inputHoldTime = 0;
        holdingKey = true;
    }

    void OnInteractionCanceled(InputAction.CallbackContext ctx)
    {
        holdingKey = false;
        inputHoldTime = 0;
    }


}

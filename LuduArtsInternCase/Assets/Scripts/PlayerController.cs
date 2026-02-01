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
    float verticalRotation;

    [Header("Interaction Settings")]
    public float interactDistance = 300;
    public Transform handTransform;
    float inputHoldTime;
    bool holdingKey;
    GameObject grabbedItem;
    GameObject rayHitItem;
    bool interactionCompleted;

    InteractionUI interactionUI;



    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Movement");
        cameraAction = playerInput.actions.FindAction("Camera");
        interactionAction = playerInput.actions.FindAction("Interaction");
        interactionAction.started += OnInteractionStarted;
        interactionAction.canceled += OnInteractionCanceled;
    }


    private void Start()
    {
        interactionUI = GameObject.FindGameObjectWithTag("HUD").GetComponent<InteractionUI>();
    }

    private void Update()
    {
        MovePlayer();
        CameraMovement();
        InteractionRay();
        
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


    void InteractionRay()
    {
        //Debug.Log("Interaction Ray");
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            var interactable = hit.collider.GetComponentInParent<I_Interaction>();
            rayHitItem = hit.collider.gameObject;

            if(holdingKey && !interactionCompleted)
            {
                InteractionAction(interactable);
            }
            else
            {
                if (interactable != null) 
                {
                    if (interactable.IsInteractable)
                    {
                        interactionUI.UpdateInteractionText(interactable.InteractionText);
                        interactionUI.ShorHoverCrosshair();
                    }
                    else
                    {
                        interactionUI.ResetInteractionWindow();
                    }
                }
                else
                {
                    interactionUI.ResetInteractionWindow();
                }
            }
        }
        else
        {
            interactionUI.ResetInteractionWindow();
            rayHitItem = null;
        }
    }


    void InteractionAction(I_Interaction interactable)
    {
        if (interactable != null)
        {
            if (interactable.IsInteractable)
            {
                if (interactable.HoldInteract)
                {
                    inputHoldTime += Time.deltaTime;
                    interactionUI.UpdateHoldingBar(inputHoldTime / interactable.HoldDuration);

                    if (inputHoldTime >= interactable.HoldDuration)
                    {
                        interactionCompleted = true;
                        holdingKey = false;
                        inputHoldTime = 0;
                        interactionUI.UpdateHoldingBar(0);

                        switch (interactable.InteractionType)
                        {
                            case ItemInteractionTypes.None:
                                break;
                            case ItemInteractionTypes.Interact:
                                interactable.OnInteract();
                                break;
                            case ItemInteractionTypes.Grab:
                                interactable.OnInteract();
                                AttachItemToHand(interactable);
                                break;
                            case ItemInteractionTypes.Combine:
                                if (grabbedItem != null)
                                {
                                    interactable.InteractedObject = grabbedItem;
                                    DropItemFromHand();
                                    interactable.OnInteract();
                                }
                                break;
                        }
                        
                        
                        
                        
                    }
                }
                else
                {
                    interactionCompleted = true;
                    switch (interactable.InteractionType)
                    {
                        case ItemInteractionTypes.None:
                            break;

                        case ItemInteractionTypes.Interact:
                            interactable.OnInteract();
                            break;

                        case ItemInteractionTypes.Grab:
                            interactable.OnInteract();
                            AttachItemToHand(interactable);
                            break;

                        case ItemInteractionTypes.Combine:
                            Debug.Log(grabbedItem == null);
                            if (grabbedItem != null)
                            {
                                interactable.InteractedObject = grabbedItem;
                                DropItemFromHand();
                                interactable.OnInteract();
                            }
                            break;
                    }
                }
            }
        }
        else
        {
            holdingKey = false;
            inputHoldTime = 0;
        }
    }


    void OnInteractionStarted(InputAction.CallbackContext ctx) // Interaction input pressed
    {
        interactionCompleted = false;
        inputHoldTime = 0;
        holdingKey = true;

        if(rayHitItem != null)
        {
            if (grabbedItem != null && rayHitItem.GetComponentInParent<I_Interaction>().InteractionType != ItemInteractionTypes.Combine)
            {
                DropItemFromHand();
            }
        }
        else
        {
            if (grabbedItem != null)
            {
                DropItemFromHand();
            }
        }
       
    }

    void OnInteractionCanceled(InputAction.CallbackContext ctx) // Interaction input released
    {
        holdingKey = false;
        inputHoldTime = 0;
        interactionUI.UpdateHoldingBar(0);
        interactionCompleted = true;
    }

    void AttachItemToHand(I_Interaction interactable) // Pickup item to hand
    {
        if(rayHitItem != null)
        {
            if (interactable.GrabableObject)
            {
                grabbedItem = rayHitItem;
                grabbedItem.transform.SetParent(handTransform);
                grabbedItem.transform.localPosition = Vector3.zero;
                grabbedItem.transform.localRotation = Quaternion.identity;
            }
        }
    }

    void DropItemFromHand() // Drop current item
    {
        if (grabbedItem != null)
        {
            var interactable = grabbedItem.GetComponentInParent<I_Interaction>();
            interactable.OnInteract();
            grabbedItem = null;
        }
        
    }


}

using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Door : InteractableBase
{
    [Header("Door Settings")]
    [SerializeField] private Transform doorPivot;
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openTime = 2f;

    private bool isOpen;
    private Quaternion closedRotation;
    private Quaternion openRotation;

  

    private void Awake()
    {
        if (doorPivot == null)
        {
            doorPivot = transform;
        }
           
        closedRotation = doorPivot.localRotation;
        openRotation = Quaternion.Euler(doorPivot.localEulerAngles + Vector3.up * openAngle
        );
    }


    private void Start()
    {
        interactionText = isOpen ? "Close" : "Open";
    }


    public override void OnInteract()
    {
        base.OnInteract();
        ToggleDoor();
    }


    private void ToggleDoor()
    {
        isOpen = !isOpen;
        StopAllCoroutines();
        StartCoroutine(RotateDoor());
        interactionText = isOpen ? "Close" : "Open";
    }


    private IEnumerator RotateDoor()
    {
        Quaternion targetRotation = isOpen ? openRotation : closedRotation;

        while (Quaternion.Angle(doorPivot.localRotation, targetRotation) > 0.1f)
        {
            doorPivot.localRotation = Quaternion.RotateTowards(doorPivot.localRotation, targetRotation, Time.deltaTime * (openAngle / openTime));
            yield return null;
        }

        doorPivot.localRotation = targetRotation;
    }
}

using UnityEngine;


public class InteractableBase : MonoBehaviour, I_Interaction
{
    [Header("InteractableSettings")]
    public float holdDuration;
    public bool holdInteract;
    public bool multipleUse;
    public bool isInteractable;

    
    // Interface

    public float HoldDuration => holdDuration;

    public bool HoldInteract => holdInteract;

    public bool MultipleUse => multipleUse;

    public bool IsInteractable => isInteractable;

    public virtual void OnInteract()
    {
       Debug.Log("Interacted");
    }


    //
   
}

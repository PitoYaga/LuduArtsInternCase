using UnityEngine;

public interface I_Interaction 
{
    ItemInteractionTypes InteractionType { get; }
    float HoldDuration { get; }
    bool HoldInteract { get; }
    bool MultipleUse { get; }
    bool IsInteractable { get; }
    string InteractionText { get; }
    bool GrabableObject { get; }
    GameObject InteractedObject { get; set; }
    ItemTypes ItemType { get; }

    void OnInteract();


}

using UnityEngine;

public interface I_Interaction 
{
    float HoldDuration { get; }
    bool HoldInteract { get; }
    bool MultipleUse { get; }
    bool IsInteractable { get; }
    string InteractionText { get; }
    bool GrabableObject { get; }

    void OnInteract();
}

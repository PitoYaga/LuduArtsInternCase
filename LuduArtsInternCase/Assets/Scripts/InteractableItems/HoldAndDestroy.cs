using UnityEngine;

public class HoldAndDestroy : InteractableBase
{
    public override void OnInteract()
    {
        base.OnInteract();
        Debug.Log("Trigger an event. Then, destroy");
        Destroy(gameObject);
    }

}

using UnityEngine;

public class GrabableItem : InteractableBase
{
    Rigidbody rigidBody;
    public bool grabbed;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }


    public override void OnInteract()
    {
        base.OnInteract();

        if (grabbed)
        {
            transform.SetParent(null);

            grabbed = !grabbed;
            rigidBody.isKinematic = false;
            isInteractable = true;
        }
        else
        {
            grabbed = !grabbed;
            rigidBody.isKinematic = true;
            isInteractable = false;
        }
    }
}

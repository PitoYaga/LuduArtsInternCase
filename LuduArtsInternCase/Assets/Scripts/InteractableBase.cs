using UnityEngine;


public class InteractableBase : MonoBehaviour, I_Interaction
{
    [Header("Interactable Settings")]
    public ItemInteractionTypes interactionType;
    public float holdDuration;
    public bool holdInteract;
    public bool multipleUse;
    public bool isInteractable;
    public string interactionText;
    public bool grabableObject;
    public GameObject interactedObject;
    public ItemTypes itemType;


    // Interface

    public float HoldDuration => holdDuration;

    public bool HoldInteract => holdInteract;

    public bool MultipleUse => multipleUse;

    public bool IsInteractable => isInteractable;

    public string InteractionText => interactionText;

    public bool GrabableObject => grabableObject;

    public ItemTypes ItemType => itemType;

    public ItemInteractionTypes InteractionType => interactionType;

    GameObject I_Interaction.InteractedObject { get => interactedObject; set => interactedObject = value; }

    public virtual void OnInteract()
    {
        //Debug.Log("Interacted");
        if(audioSource != null && interactionSound != null)
        {
            audioSource.PlayOneShot(interactionSound);
        }
        if (interactionParticle != null)
        {
            Instantiate(interactionParticle, particleEffectPivot? particleEffectPivot.position : transform.position , Quaternion.identity);
        }
    }


    //


    [Header("Interaction Effects")]
    public AudioClip interactionSound;
    public ParticleSystem interactionParticle;
    public Transform particleEffectPivot;

    


    //Components
    AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

}

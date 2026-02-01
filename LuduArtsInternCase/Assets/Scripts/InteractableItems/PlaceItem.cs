using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceItem : InteractableBase
{
    public Transform itemAttachTransform;
    public ItemTypes targetItemType;
    public GameObject newItem;


    public override void OnInteract()
    {
        if(interactedObject.GetComponentInParent<I_Interaction>().ItemType == targetItemType)
        {
            base.OnInteract();
            interactedObject.GetComponentInParent<I_Interaction>().OnInteract();
            interactedObject.transform.SetParent(itemAttachTransform);
            interactedObject.transform.localPosition = Vector3.zero;
            interactedObject.transform.localRotation = Quaternion.identity;
            StartCoroutine(ChangeItem());
            isInteractable = false;
        }
      
    }

    IEnumerator ChangeItem()
    {
        yield return new WaitForSeconds(2);
        Destroy(interactedObject);
        GameObject spawnedItem = Instantiate(newItem, itemAttachTransform.position, Quaternion.identity);
        //spawnedItem.GetComponentInParent<I_Interaction>().OnInteract();
        spawnedItem.transform.SetParent(itemAttachTransform);
        spawnedItem.transform.localPosition = Vector3.zero;
        spawnedItem.transform.localRotation = Quaternion.identity;
        isInteractable = true;
    }

}

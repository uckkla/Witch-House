using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager;
using UnityEngine;

interface Interactable {
    public void Interact();
}

public class Interactor : MonoBehaviour {
    [SerializeField] private Transform interacterSource;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private float interactRange;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private CauldronCollision cauldronCollision;

    private ObjectGrabbable objectGrabbable;
    private Potion holdingPotion;

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (objectGrabbable == null) {
                Ray r = new Ray(interacterSource.position, interacterSource.forward);
                if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange)) {
                    // Animation type interactions
                    Debug.Log("Hit something");
                    if (hitInfo.transform.gameObject.TryGetComponent(out Interactable interactObj)) {
                        Debug.Log("Interacting...");
                        interactObj.Interact();
                    }
                    // Grabbing
                    else if (hitInfo.transform.gameObject.TryGetComponent(out objectGrabbable)) {
                        Debug.Log("Picking up!");
                        if (hitInfo.transform.gameObject.TryGetComponent(out Potion potion)) {
                            Debug.Log("Picking up a potion!");
                            holdingPotion = potion;
                        }
                        objectGrabbable.Grab(objectGrabPointTransform);
                    }
                }
            }
            // Drop item
            else {
                Debug.Log("Dropping...");
                objectGrabbable.Drop();
                objectGrabbable = null;
            }
        }
        // Fill up potion
        /* Checks for:
         * - Left Click
         * - If bottle is inside liquid
         * - If potion being held is same as potion colliding
         */
        else if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Debug.Log("Pressing left click!");
            if (cauldronCollision.isPotionInCauldron && cauldronCollision.getCollidingPotion == holdingPotion) {
                Debug.Log("Putting drink in potion");
                holdingPotion.ToggleFill(cauldronCollision.getCurrentRecipe);
            }
            else {
                Ray r = new Ray(interacterSource.position, interacterSource.forward);
                if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange)) {
                    Debug.Log("Hitting item...");
                    if (hitInfo.transform.gameObject.TryGetComponent(out ItemSpawning itemSpawning)) {
                        itemSpawning.SpawnItem();
                    }
                }
            }
        }
        // Consume potion
        else if (Input.GetKeyDown(KeyCode.Mouse1) && holdingPotion) {
            if (holdingPotion.getCurrentRecipe != null) {
                Debug.Log("Applying effect");
                player.ApplyEffect(holdingPotion.getCurrentRecipe);
                holdingPotion.ToggleFill();
            }
        }
    }
}
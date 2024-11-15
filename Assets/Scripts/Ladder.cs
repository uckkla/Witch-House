using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Debug.Log("Triggered enter!");
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement)) {
            Debug.Log("Triggerer was player! Climbing...");
            playerMovement.setCurrentState(PlayerState.Climbing);
        }
    }

    // When player tries to get off top of the ladder
    private void OnTriggerExit(Collider other) {
        Debug.Log("Trigger leave!");
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement)) {
            Debug.Log("Triggerer was player! Stopped climbing...");
            playerMovement.setCurrentState(PlayerState.Walking);
        }
    }
}

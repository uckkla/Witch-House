using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour, Interactable {

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip openAudio, closeAudio;
    private bool isOpen = false;

    public void Interact() {
        isOpen = !isOpen;
        source.PlayOneShot(isOpen ? openAudio : closeAudio);
        GetComponent<Animator>().SetBool("open", isOpen);
    }
}

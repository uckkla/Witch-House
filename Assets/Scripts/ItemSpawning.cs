using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawning : MonoBehaviour {
    [SerializeField] private GameObject item;
    [SerializeField] private Transform itemSpawnPoint;
    private AudioSource source;

    private void Awake() {
        source = GetComponent<AudioSource>();
    }

    // NEED TO CHANGE THIS, inefficient to have each item hold the audio source and clip.
    public void SpawnItem() {
        Debug.Log("Spawning item...");
        Instantiate(item, itemSpawnPoint.position, Quaternion.identity);
        source.Play();
    }
}

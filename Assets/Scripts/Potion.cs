using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UIElements;

public enum PotionEffect{
    None,
    SpeedBoost,
    JumpBoost,
    SizeIncrease,
    SizeDecrease,
    Teleporation
}

public class Potion : MonoBehaviour
{
    private bool isFilled = false;
    private Recipe currentRecipe = null;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip drink, fill; // In future use OpenObject, however no animation at the moment
    [SerializeField] private Renderer liquidRenderer; 

    public void ToggleFill(Recipe newRecipe = null) {
        isFilled = !isFilled;

        // Bug - because it isnt checked if the fill or empty is passing, you can end up emptying it while it is in the cauldron by trying to fill.
        // Can be a feature if intended to empty the bottle without drinking
        if (isFilled) {
            Debug.Log("Filling bottle");
            currentRecipe = newRecipe;
            liquidRenderer.material.color = newRecipe.getEffectColour;
            liquidRenderer.gameObject.SetActive(true);
            source.PlayOneShot(fill);
        }
        else {
            currentRecipe = null;
            liquidRenderer.material.color = Color.clear;
            liquidRenderer.gameObject.SetActive(false);
            source.PlayOneShot(drink);
        }
    }

    public Recipe getCurrentRecipe => currentRecipe;
}

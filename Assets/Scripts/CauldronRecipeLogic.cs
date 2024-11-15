using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class CauldronRecipeLogic : MonoBehaviour {

    private HashSet<ingredientType> currentIngredients = new HashSet<ingredientType>();
    [SerializeField] private List<Recipe> recipes;
    [SerializeField] private Renderer liquidRenderer;
    [SerializeField] ParticleSystem bubbleParticle;
    [SerializeField] private PlayerMovement player;

    private Recipe currentRecipe; // Valid recipe in cauldron
    private GameObject bottle; // maybe
    private Color poisonColour = new Color(137f/255f, 255f/255f, 81f/255f);

    public void AddIngredient(ingredientType ingredient) {
        currentIngredients.Add(ingredient);
        GetComponent<AudioSource>().Play(); // Play sizzling sound
        CheckRecipe();
        foreach (var value in currentIngredients) {
            Debug.Log(value);
        }
    }

    public void ClearIngredients() {
        currentIngredients.Clear();
        GetComponent<AudioSource>().Play(); // Play sizzling sound
        currentRecipe = null;
        
        var bubbleParticleMain = bubbleParticle.main;
        bubbleParticleMain.startColor = poisonColour;
        liquidRenderer.material.color = poisonColour;

        foreach (var value in currentIngredients) {
            Debug.Log(value);
        }
    }

    private void CheckRecipe() {
        foreach (Recipe recipe in recipes){
            if (currentIngredients.SetEquals(recipe.getIngredients)){
                Debug.Log("ALL CURRENT INGREDIENTS -----------");
                foreach (var value1 in currentIngredients) {
                    Debug.Log(value1);
                }
                Debug.Log("--------------------------");

                var bubbleParticleMain = bubbleParticle.main;
                bubbleParticleMain.startColor = recipe.getEffectColour;
                liquidRenderer.material.color = recipe.getEffectColour;
                currentRecipe = recipe;
                return;
            }
        }
    }

    public Recipe getCurrentRecipe => currentRecipe;
}
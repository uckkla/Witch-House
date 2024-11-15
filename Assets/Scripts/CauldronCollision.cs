using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronCollision : MonoBehaviour
{
    [SerializeField] private CauldronRecipeLogic cauldron;
    private bool potionInCauldron = false;
    private Potion collidingPotion = null;

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Ingredient ingredient)) {
            if (ingredient.getIngredientName() == ingredientType.PurifiedCrystal) {
                cauldron.ClearIngredients();
            }
            else {
                cauldron.AddIngredient(ingredient.getIngredientName());
            }
            Destroy(other.gameObject);
        }
        else if (other.TryGetComponent(out Potion potion)) {
            Debug.Log("Potion is in cauldron");
            potionInCauldron = true;
            collidingPotion = potion;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.TryGetComponent(out Potion potion)) {
            Debug.Log("Potion is out of cauldron");
            potionInCauldron = false;
            collidingPotion = null;
        }
    }

    public bool isPotionInCauldron => potionInCauldron;

    public Potion getCollidingPotion => collidingPotion;

    public Recipe getCurrentRecipe => cauldron.getCurrentRecipe;
}

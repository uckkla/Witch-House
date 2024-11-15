using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ingredientType {
    Eye,
    Worm,
    Mushroom,
    PurifiedCrystal
}

public class Ingredient : MonoBehaviour {
    [SerializeField] private ingredientType ingredientName;

    public ingredientType getIngredientName() {
        return ingredientName;
    }
}

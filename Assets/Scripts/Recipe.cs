using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Recipe
{
    [SerializeField] private List<ingredientType> ingredients;
    [SerializeField] private Color effectColour;
    [SerializeField] private PotionEffect potionEffect; // in Potion.cs

    [SerializeField] private float speedMultiplier;
    [SerializeField] private float jumpForceMultiplier;
    [SerializeField] private float sizeMultiplier; // multiply if increase, divide if decrease
    [SerializeField] private float effectDuration;

    public Recipe(List<ingredientType> ingredients, Color effectColour, float speedMultiplier,
        float jumpForceMultiplier, float sizeMultiplier) {
        this.ingredients = ingredients;
        this.effectColour = effectColour;
        this.speedMultiplier = speedMultiplier;
        this.jumpForceMultiplier = jumpForceMultiplier;
        this.sizeMultiplier = sizeMultiplier;
    }

    public List<ingredientType> getIngredients => this.ingredients;

    public Color getEffectColour => this.effectColour;

    public PotionEffect getPotionEffect => this.potionEffect;

    public float getSpeedMultiplier => this.speedMultiplier;

    public float getJumpForceMultiplier => this.jumpForceMultiplier;

    public float getSizeMultiplier => this.sizeMultiplier;

    public float getEffectDuration => this.effectDuration;

}

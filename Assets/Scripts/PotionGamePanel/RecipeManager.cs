using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecipeManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI recipeText; // Displays the recipe
    [SerializeField] private GameObject recipePanel; // Recipe display panel
    [SerializeField] private TextMeshProUGUI feedbackText; // Feedback for correct/incorrect ingredients
    [SerializeField] private TextMeshProUGUI candyCounterText; // Displays candy count
    [SerializeField] private GameObject potionMiniGamePanel; // Mini-game panel for the potion

    private CandyCollection candyCollection; // Reference to the CandyCollection script
    private ItemSoundManager soundManager; // Reference to the ItemSoundManager script

    [Header("Cauldron Area")]
    [SerializeField] private Transform cauldronArea; // Drop zone for ingredients

    private string[] currentRecipe; // Stores the steps of the current recipe
    private Dictionary<string, int> currentRecipeQuantities; // Tracks required quantities for each step
    private int currentStep = 0; // Tracks the current step in the recipe
    private string[] ingredients = { "Bat", "Eyeball", "Spider", "Blood", "Pumpkin" }; // Ingredient pool

    [Header("Audio Clips")]
    public AudioClip correctCandySound; // Sound for correct candy placement
    public AudioClip incorrectCandySound; // Sound for incorrect candy placement

    private void Start()
    {
        candyCollection = FindObjectOfType<CandyCollection>();
        soundManager = FindObjectOfType<ItemSoundManager>();

        if (candyCollection == null)
        {
            Debug.LogError("CandyCollection script not found in the scene!");
        }

        if (soundManager == null)
        {
            Debug.LogError("ItemSoundManager not found in the scene!");
        }

        GenerateNewRecipe();
    }

    public void ViewRecipe()
    {
        recipePanel.SetActive(true);
        Invoke("HideRecipe", 3f); // Show recipe for 3 seconds
    }

    private void HideRecipe()
    {
        recipePanel.SetActive(false);
    }

    private void GenerateNewRecipe()
    {
        int numberOfSteps = Random.Range(3, 5); // Randomize 3-4 steps
        currentRecipe = new string[numberOfSteps];
        currentRecipeQuantities = new Dictionary<string, int>();
        currentStep = 0; // Reset current step

        List<string> usedIngredients = new List<string>();

        for (int i = 0; i < numberOfSteps; i++)
        {
            string ingredient;
            do
            {
                ingredient = ingredients[Random.Range(0, ingredients.Length)];
            } while (usedIngredients.Contains(ingredient)); // Ensure unique ingredients

            usedIngredients.Add(ingredient);

            int quantity = Random.Range(1, 5); // Random quantity between 1 and 4

            currentRecipe[i] = $"Add {quantity} {ingredient}" + (quantity > 1 ? "s" : "");
            currentRecipeQuantities[ingredient] = quantity; // Store required quantity
        }

        recipeText.text = string.Join("\n", currentRecipe); // Update recipe UI
    }

    public bool AddIngredient(string ingredient)
    {
        if (currentStep >= currentRecipe.Length)
        {
            feedbackText.text = "You've already completed the recipe!";
            return false;
        }

        string expectedStep = currentRecipe[currentStep];
        string expectedIngredient = ExtractIngredientName(expectedStep);
        int requiredQuantity = currentRecipeQuantities[expectedIngredient];

        if (ingredient == expectedIngredient)
        {
            currentRecipeQuantities[expectedIngredient]--; // Decrement the required quantity
            soundManager?.PlaySpecificSound(correctCandySound);

            if (currentRecipeQuantities[expectedIngredient] <= 0)
            {
                currentStep++; // Move to the next step
                feedbackText.text = $"{expectedIngredient} complete!";

                if (currentStep == currentRecipe.Length)
                {
                    CompleteRecipe();
                }
            }
            else
            {
                feedbackText.text = $"You need {currentRecipeQuantities[expectedIngredient]} more {expectedIngredient}(s)!";
            }
            return true;
        }
        else
        {
            feedbackText.text = $"Wrong ingredient! You need {expectedIngredient} next!";
            soundManager?.PlaySpecificSound(incorrectCandySound);
            return false;
        }
    }

    private void CompleteRecipe()
    {
        feedbackText.text = "Potion Complete! You've earned candy!";

        // Add candies to the collection
        if (candyCollection != null)
        {
            candyCollection.CollectCandy(); // Add 1 candy
            candyCollection.CollectCandy(); // Add another candy
        }
        else
        {
            Debug.LogError("CandyCollection reference is missing!");
        }

        // Close the mini-game panel
        if (potionMiniGamePanel != null)
        {
            potionMiniGamePanel.SetActive(false);
        }

        Invoke(nameof(GenerateNewRecipe), 20f); // Generate a new recipe after 2 seconds
    }

    private string ExtractIngredientName(string recipeStep)
    {
        string[] parts = recipeStep.Split(' ');
        return parts[2].TrimEnd('s'); // Extract ingredient name and remove plural 's'
    }

    public Transform GetCauldronArea()
    {
        return cauldronArea;
    }
}
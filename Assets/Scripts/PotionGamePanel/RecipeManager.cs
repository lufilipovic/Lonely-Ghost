using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecipeManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI recipeText;   // TextMeshPro for the recipe
    [SerializeField] private GameObject recipePanel;      // Recipe display panel
    [SerializeField] private TextMeshProUGUI feedbackText; // TextMeshPro for feedback
    [SerializeField] private TextMeshProUGUI candyCounterText; // TextMeshPro for candy counter
    [SerializeField] private GameObject potionMiniGamePanel;

    private CandyCollection candyCollection; // Reference to the CandyCollection script


    [Header("Cauldron")]
    [SerializeField] private Transform cauldronArea;

    private string[] currentRecipe; // Stores the steps of the current recipe
    private int currentStep = 0;   // Tracks the current step in the recipe
    private int candyCount = 0;    // Tracks candies earned

    private Dictionary<string, int> currentRecipeQuantities; // Tracks required quantities for each step
    private Dictionary<string, int> addedQuantities; // Tracks how many of each ingredient have been added

    private string[] ingredients = { "Bat", "Eyeball", "Spider", "Hat", "Pumpkin" }; // Ingredient pool

    private void Start()
    {
        // Find the CandyCollection script in the scene
        candyCollection = FindObjectOfType<CandyCollection>();

        if (candyCollection == null)
        {
            Debug.LogError("CandyCollection script not found in the scene!");
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
        int numberOfSteps = Random.Range(2, 5); // Randomize 2-4 steps
        currentRecipe = new string[numberOfSteps];
        currentRecipeQuantities = new Dictionary<string, int>();
        addedQuantities = new Dictionary<string, int>();

        List<string> usedIngredients = new List<string>();

        for (int i = 0; i < numberOfSteps; i++)
        {
            // Select a random ingredient
            string ingredient;
            do
            {
                ingredient = ingredients[Random.Range(0, ingredients.Length)];
            } while (usedIngredients.Contains(ingredient)); // Ensure unique ingredients

            usedIngredients.Add(ingredient);

            // Select a random quantity
            int quantity = Random.Range(1, 4); // Random quantity between 1 and 3

            // Create the recipe step
            currentRecipe[i] = $"Add {quantity} {ingredient}" + (quantity > 1 ? "s" : "");
            currentRecipeQuantities[ingredient] = quantity; // Store required quantity
        }

        recipeText.text = string.Join("\n", currentRecipe); // Update recipe UI
        currentStep = 0; // Reset steps
    }

    public bool AddIngredient(string ingredient)
    {
        if (currentStep >= currentRecipe.Length)
        {
            feedbackText.text = "You've added all the ingredients!";
            return false;
        }

        // Get the current ingredient and required quantity
        string expected = currentRecipe[currentStep];
        string expectedIngredient = ExtractIngredientName(expected);
        int requiredQuantity = currentRecipeQuantities[expectedIngredient];

        // Increment the added quantity for the ingredient
        if (!addedQuantities.ContainsKey(expectedIngredient))
        {
            addedQuantities[expectedIngredient] = 0;
        }
        addedQuantities[expectedIngredient]++;

        // Check if the added quantity meets the required quantity
        if (expectedIngredient == ingredient && addedQuantities[expectedIngredient] >= requiredQuantity)
        {
            feedbackText.text = "Correct!";
            currentStep++;

            // Check if the recipe is complete
            if (currentStep == currentRecipe.Length)
            {
                CompleteRecipe(); // Handle recipe completion
            }
            return true; // Ingredient and quantity were correct
        }
        else if (expectedIngredient == ingredient)
        {
            feedbackText.text = $"You need {requiredQuantity - addedQuantities[expectedIngredient]} more!";
            return true; // Correct ingredient but not enough quantity yet
        }
        else
        {
            feedbackText.text = "Wrong ingredient! Try again!";
            return false; // Incorrect ingredient
        }
    }

    private void CompleteRecipe()
    {
        feedbackText.text = "Potion Complete! You've earned a candy!";
        candyCount++;
        candyCounterText.text = "Candy: " + candyCount;

        if (potionMiniGamePanel != null)
        {
            candyCollection.CollectCandy();
            potionMiniGamePanel.SetActive(false);
        }
        else
        {
            Debug.LogError("MiniGamePanel is not assigned!");
        }

        // Generate a new recipe after a short delay
        Invoke("GenerateNewRecipe", 2f);
    }

    private string ExtractIngredientName(string recipeStep)
    {
        // Extract ingredient name from recipe step by splitting the string
        string[] parts = recipeStep.Split(' ');
        return parts[parts.Length - 1].TrimEnd('s'); // Trim 's' for plural forms
    }

    public Transform GetCauldronArea()
    {
        return cauldronArea;
    }
}
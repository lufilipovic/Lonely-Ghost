using UnityEngine;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    // Dictionary to store panels by name
    private Dictionary<string, GameObject> panels = new Dictionary<string, GameObject>();

    private void Start()
    {
        // Find and store all panels in the scene
        foreach (Transform child in transform)
        {
            GameObject panel = child.gameObject;
            panels[panel.name] = panel; // Store each panel with its name as the key
            panel.SetActive(false); // Ensure all panels start inactive
        }
    }

    // Show a specific panel by name
    public void ShowPanel(string panelName)
    {
        if (panels.ContainsKey(panelName))
        {
            panels[panelName].SetActive(true); // Activate the specified panel
            Time.timeScale = 0; // Pause the game if needed
        }
        else
        {
            Debug.LogWarning($"Panel '{panelName}' not found in UIManager.");
        }
    }

    // Close a specific panel by name
    public void ClosePanel(string panelName)
    {
        if (panels.ContainsKey(panelName))
        {
            panels[panelName].SetActive(false); // Deactivate the specified panel
            Time.timeScale = 1; // Resume the game if needed
        }
        else
        {
            Debug.LogWarning($"Panel '{panelName}' not found in UIManager.");
        }
    }

    // Example method to set text on a specific panel, assuming it has a Text component
    public void SetPanelText(string panelName, string text)
    {
        if (panels.ContainsKey(panelName))
        {
            var textComponent = panels[panelName].GetComponentInChildren<UnityEngine.UI.Text>();
            if (textComponent != null)
            {
                textComponent.text = text;
            }
            else
            {
                Debug.LogWarning($"No Text component found in panel '{panelName}'.");
            }
        }
        else
        {
            Debug.LogWarning($"Panel '{panelName}' not found in UIManager.");
        }
    }
}

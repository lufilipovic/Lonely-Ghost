using UnityEngine;

public class ExitConfirmation : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject confirmationPanel;     // Exit confirmation panel
    [SerializeField] private GameObject yesButton;             // Yes button
    [SerializeField] private GameObject noButton;              // No button

    private GameObject currentPanel; // The panel to be managed
    private bool canReopenPanel = true; // Flag to prevent reopening after exit

    private void Start()
    {
        // Ensure the confirmation panel is hidden at the start
        confirmationPanel.SetActive(false);
    }

    public void ShowExitConfirmation(GameObject panel)
    {
        if (!canReopenPanel) return; // Prevent reopening if panel is already exited

        currentPanel = panel; // Set the current panel to be managed
        confirmationPanel.SetActive(true); // Show the confirmation panel
    }

    public void OnYesButtonPressed()
    {
        if (currentPanel != null)
        {
            currentPanel.SetActive(false); // Close the current panel
        }

        confirmationPanel.SetActive(false); // Close the confirmation panel
        //canReopenPanel = false; // Prevent reopening of the panel

        Debug.Log("Panel has been closed permanently.");
    }

    public void OnNoButtonPressed()
    {
        confirmationPanel.SetActive(false); // Close the confirmation panel only
        Debug.Log("Exit canceled.");
    }
}
using UnityEngine;

public class FinishGameTrigger : MonoBehaviour
{
    public GameEndingManager gameEndingManager; // Reference to the GameEndingManager

    private void OnMouseDown()
    {
        if (gameEndingManager != null)
        {
            gameEndingManager.ShowConfirmationPanel();
        }
    }
}
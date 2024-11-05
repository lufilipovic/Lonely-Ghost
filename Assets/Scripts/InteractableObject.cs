using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    public GameObject panel; // Assign the panel in the inspector

    void Start()
    {
        if (panel != null)
        {
            panel.SetActive(false); // Hide the panel initially
        }
    }

    void OnMouseDown()
    {
        if (panel != null)
        {
            panel.SetActive(true); // Show the panel when the prefab is clicked
        }
    }

    // Method to hide the panel, which you can assign to a button's OnClick event
    public void HidePanel()
    {
        if (panel != null)
        {
            panel.SetActive(false); // Hide the panel when this function is called
        }
    }
}

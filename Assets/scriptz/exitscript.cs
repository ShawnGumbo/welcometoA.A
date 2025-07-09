
// This script handles the functionality for an "Escape" or "Quit" button in Unity.

using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management
using UnityEngine.UI; // Required for UI components like Button

public class exitscript : MonoBehaviour
{
    // Public string variable to specify the name of the main menu scene to load.
    // Leave empty if you intend to quit the application instead.
    public string mainMenuSceneName = "MainMenu"; // Default name for your main menu scene

    // Boolean to determine if the button should quit the application instead of loading a scene.
    public bool quitApplication = false;

    private Button button; // Reference to the Button component

    void Start()
    {
        // Get the Button component attached to this GameObject.
        button = GetComponent<Button>();

        if (button != null)
        {
            // Add a listener to the button's onClick event.
            // When the button is clicked, the HandleEscapeAction method will be called.
            button.onClick.AddListener(HandleEscapeAction);
            Debug.Log("EscapeButton: Listener added to button.");
        }
        else
        {
            Debug.LogError("EscapeButton: No Button component found on this GameObject. Please attach this script to a GameObject with a Button component.");
        }

        // Important: If loading a scene, ensure it's in File -> Build Settings.
    }

    // This method is called when the button is clicked.
    public void HandleEscapeAction()
    {
        if (quitApplication)
        {
            // If 'quitApplication' is true, exit the game.
            Debug.Log("EscapeButton: Quitting application...");
            Application.Quit();

            // Note: Application.Quit() only works in a built game.
            // In the Unity editor, it will stop Play mode.
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        else if (!string.IsNullOrEmpty(mainMenuSceneName))
        {
            // If 'quitApplication' is false and a scene name is provided, load that scene.
            Debug.Log($"EscapeButton: Loading Main Menu scene: {mainMenuSceneName}");
            SceneManager.LoadScene(mainMenuSceneName);
        }
        else
        {
            // Error if neither option is properly configured.
            Debug.LogWarning("EscapeButton: Neither 'Quit Application' is checked nor 'Main Menu Scene Name' is set. Button will do nothing.");
        }
    }

    // Good practice: Remove listeners when the GameObject is destroyed.
    void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(HandleEscapeAction);
        }
    }
}
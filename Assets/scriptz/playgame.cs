// SceneLoader.cs
// This script handles loading different scenes when a UI Button is clicked.

using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management functionalities
using UnityEngine.UI; // Required for UI components like Button

public class SceneLoader : MonoBehaviour
{
    // Public string variable to specify the name of the scene to load.
    // You will type the name of your target scene (e.g., "GameScene", "MainMenu")
    // into this field in the Unity Inspector.
    public string sceneToLoad="Gameplayscene";

    // Optional: Reference to the button component itself.
    // This is useful if you want to add the listener programmatically.
    private Button button;

    void Start()
    {
        // Get the Button component attached to this GameObject.
        // This assumes the script is attached to the same GameObject as the Button.
        button = GetComponent<Button>();

        // Check if a Button component was found.
        if (button != null)
        {
            // Add a listener to the button's onClick event.
            // When the button is clicked, the LoadTargetScene method will be called.
            button.onClick.AddListener(LoadTargetScene);
            Debug.Log($"SceneLoader: Button listener added for scene '{sceneToLoad}'.");
        }
        else
        {
            Debug.LogError("SceneLoader: No Button component found on this GameObject. Please attach this script to a GameObject with a Button component.");
        }

        // IMPORTANT: Ensure the scene you want to load is added to the Build Settings.
        // Go to File -> Build Settings... and drag your scenes into the "Scenes In Build" list.
        // If the scene is not in the Build Settings, it cannot be loaded by name.
    }

    // This public method will be called when the button is clicked.
    public void LoadTargetScene()
    {
        // Check if the scene name is provided.
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.Log($"SceneLoader: Attempting to load scene: {sceneToLoad}");
            // Load the specified scene asynchronously for a smoother experience,
            // or use SceneManager.LoadScene(sceneToLoad); for direct loading.
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("SceneLoader: No scene name specified in 'sceneToLoad' variable. Please set it in the Inspector.");
        }
    }

    // Good practice: Remove listeners when the GameObject is destroyed to prevent memory leaks.
    void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(LoadTargetScene);
        }
    }
}


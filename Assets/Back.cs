using UnityEngine;
using UnityEngine.SceneManagement; // Essential for loading scenes
using UnityEngine.UI; // Essential for interacting with UI Button components
// using TMPro; // Uncomment this line if your button uses TextMeshPro for its text

public class Back : MonoBehaviour
{
    // Public string variable to specify the name of the scene to load.
    // You will set this in the Unity Editor's Inspector.
    // Example: "MainMenu", "Level1", "CreditsScene", "TutorialsScene"
    [Tooltip("Enter the exact name of the scene to load when this button is clicked.")]
    public string targetSceneName = "Main Menu"; // *** IMPORTANT: CHANGE THIS IN THE INSPECTOR ***

    // A private reference to the Button component this script will be attached to.
    private Button creditsAndTutorialsButton;

    void Start()
    {
        // Get the Button component that is on the same GameObject as this script.
        creditsAndTutorialsButton = GetComponent<Button>();

        // Check if a Button component was found. If not, log an error.
        if (creditsAndTutorialsButton != null)
        {
            // Add a listener to the button's onClick event.
            // This means when the button is clicked, the LoadTargetScene() method will be called.
            creditsAndTutorialsButton.onClick.AddListener(LoadTargetScene);
            Debug.Log($"CreditsAndTutorialsSceneChanger: Listener added to button. Will attempt to load scene: '{targetSceneName}'");
        }
        else
        {
            // If there's no Button component, this script can't function correctly.
            Debug.LogError("CreditsAndTutorialsSceneChanger: No Button component found on this GameObject. Please attach this script to a GameObject with a Button component.");
        }

        // REMINDER: For a scene to be loadable by name, it MUST be added to your Build Settings.
        // Go to File -> Build Settings... and drag your scene files (.unity) into the "Scenes In Build" list.
    }

    /// <summary>
    /// This method is called when the button is clicked.
    /// It attempts to load the scene specified in 'targetSceneName'.
    /// </summary>
    public void LoadTargetScene()
    {
        // Basic validation: Check if a target scene name has been provided.
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            Debug.Log($"CreditsAndTutorialsSceneChanger: Loading scene: '{targetSceneName}'...");
            // Load the specified scene.
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            // If targetSceneName is empty, inform the user to set it in the Inspector.
            Debug.LogError("CreditsAndTutorialsSceneChanger: Target Scene Name is not set! Please specify the scene name in the Inspector.");
        }
    }

    // Good practice: Remove the event listener when the GameObject is destroyed
    // to prevent potential memory leaks or null reference errors if the button persists.
    void OnDestroy()
    {
        if (creditsAndTutorialsButton != null)
        {
            creditsAndTutorialsButton.onClick.RemoveListener(LoadTargetScene);
            Debug.Log("CreditsAndTutorialsSceneChanger: Listener removed.");
        }
    }
}
using UnityEngine;
using UnityEngine.UI; // Required for UI components like Button
using UnityEngine.SceneManagement; // Required for scene management
using TMPro; // Required for TextMeshProUGUI if using TextMeshPro buttons

public class levelselectiobdropdown : MonoBehaviour
{
    // Public reference to the GameObject that acts as the dropdown panel.
    // This panel should contain your Level 1, Level 2, Level 3 buttons.
    // Drag the panel GameObject into this slot in the Unity Inspector.
    public GameObject levelDropdownPanel;

    // Reference to the main "Play Game" button this script is attached to.
    private Button playButton;

    void Start()
    {
        // Get the Button component attached to this GameObject.
        playButton = GetComponent<Button>();

        if (playButton != null)
        {
            // Add a listener to the main "Play Game" button's onClick event.
            playButton.onClick.AddListener(ToggleDropdown);
            Debug.Log("LevelSelectionDropdown: Play button listener added.");
        }
        else
        {
            Debug.LogError("LevelSelectionDropdown: No Button component found on this GameObject. Attach this script to your 'Play Game' button.");
        }

        // Ensure the dropdown panel is initially inactive.
        if (levelDropdownPanel != null)
        {
            levelDropdownPanel.SetActive(false);
            Debug.Log("LevelSelectionDropdown: Level dropdown panel initialized as inactive.");
        }
        else
        {
            Debug.LogError("LevelSelectionDropdown: 'Level Dropdown Panel' is not assigned! Please assign the GameObject containing your level buttons in the Inspector.");
        }
    }

    // Toggles the active state of the level dropdown panel.
    public void ToggleDropdown()
    {
        if (levelDropdownPanel != null)
        {
            // Invert the active state: if active, make inactive; if inactive, make active.
            levelDropdownPanel.SetActive(!levelDropdownPanel.activeSelf);
            Debug.Log($"LevelSelectionDropdown: Dropdown panel visibility toggled to: {levelDropdownPanel.activeSelf}");
        }
    }

    // --- Methods for loading specific levels ---
    // These methods will be assigned to the OnClick() events of your individual level buttons.

    public void LoadLevel1()
    {
        LoadSpecificLevel("Gameplayscene");
    }

    public void LoadLevel2()
    {
        LoadSpecificLevel("Level 2");
    }

    public void LoadLevel3()
    {
        LoadSpecificLevel("Final Level");
    }

    // Generic method to load a scene by name.
    private void LoadSpecificLevel(string levelName)
    {
        if (!string.IsNullOrEmpty(levelName))
        {
            Debug.Log($"LevelSelectionDropdown: Loading scene: {levelName}");
            SceneManager.LoadScene(levelName);
        }
        else
        {
            Debug.LogError("LevelSelectionDropdown: Attempted to load an empty level name.");
        }
    }

    // Good practice: Remove listeners when the GameObject is destroyed.
    void OnDestroy()
    {
        if (playButton != null)
        {
            playButton.onClick.RemoveListener(ToggleDropdown);
        }
    }
}


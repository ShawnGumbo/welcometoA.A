using UnityEngine;
using UnityEngine.UI; // Required for UI components like Button
using TMPro; // Required for TextMeshProUGUI

public class SoundOptionsButton : MonoBehaviour
{
    // Reference to the Button component this script is attached to.
    private Button soundToggleButton;

    // Private references to the text components on the button.
    // The script will try to find these automatically.
    private TextMeshProUGUI tmProText; // For TextMeshPro
    private Text legacyText;           // For legacy UI Text

    // Key for PlayerPrefs to save/load sound setting.
    private const string SOUND_SETTING_KEY = "SoundEnabled";

    void Start()
    {
        // Get the Button component attached to this GameObject.
        soundToggleButton = GetComponent<Button>();

        if (soundToggleButton != null)
        {
            // Add a listener to the button's onClick event.
            soundToggleButton.onClick.AddListener(ToggleSound);
            Debug.Log("SoundOptionsButton: Listener added to button.");

            // Try to find TextMeshProUGUI first (recommended for modern UI)
            tmProText = GetComponentInChildren<TextMeshProUGUI>();
            if (tmProText != null)
            {
                Debug.Log("SoundOptionsButton: Found TextMeshProUGUI component automatically.");
            }
            else
            {
                // If TextMeshProUGUI not found, try legacy UI Text
                legacyText = GetComponentInChildren<Text>();
                if (legacyText != null)
                {
                    Debug.Log("SoundOptionsButton: Found legacy UI Text component automatically.");
                }
                else
                {
                    Debug.LogWarning("SoundOptionsButton: No TextMeshProUGUI or legacy UI Text component found as a child of the button. Button text will not update.");
                }
            }

            // Load the saved sound setting and apply it.
            LoadSoundSetting();
        }
        else
        {
            Debug.LogError("SoundOptionsButton: No Button component found on this GameObject. Please attach this script to a GameObject with a Button component.");
        }

        // Update the button's text initially based on the current sound state.
        UpdateButtonText();
    }

    // Toggles the global sound volume (AudioListener.volume) on/off.
    public void ToggleSound()
    {
        // If sound is currently enabled (volume > 0), disable it.
        // Otherwise, enable it.
        bool soundWasEnabled = (AudioListener.volume > 0.0f);
        if (soundWasEnabled)
        {
            AudioListener.volume = 0.0f; // Mute sound
            Debug.Log("SoundOptionsButton: Sound disabled.");
        }
        else
        {
            AudioListener.volume = 1.0f; // Enable sound (full volume)
            Debug.Log("SoundOptionsButton: Sound enabled.");
        }

        // Save the new sound setting.
        SaveSoundSetting(!soundWasEnabled); // Save true if now enabled, false if now disabled

        // Update the button's text to reflect the new state.
        UpdateButtonText();
    }

    // Saves the current sound setting to PlayerPrefs.
    private void SaveSoundSetting(bool enabled)
    {
        // PlayerPrefs stores 1 for true, 0 for false.
        PlayerPrefs.SetInt(SOUND_SETTING_KEY, enabled ? 1 : 0);
        PlayerPrefs.Save(); // Ensure the data is written to disk immediately
    }

    // Loads the sound setting from PlayerPrefs and applies it.
    private void LoadSoundSetting()
    {
        // GetInt returns 0 if the key doesn't exist, which we treat as sound enabled by default.
        int savedSetting = PlayerPrefs.GetInt(SOUND_SETTING_KEY, 1); // Default to 1 (sound enabled)

        if (savedSetting == 1)
        {
            AudioListener.volume = 1.0f; // Enable sound
            Debug.Log("SoundOptionsButton: Loaded sound setting - Enabled.");
        }
        else
        {
            AudioListener.volume = 0.0f; // Disable sound
            Debug.Log("SoundOptionsButton: Loaded sound setting - Disabled.");
        }
    }

    // Updates the text on the button based on the current sound state.
    private void UpdateButtonText()
    {
        if (tmProText != null)
        {
            // If TextMeshProUGUI is found, update its text.
            if (AudioListener.volume > 0.0f)
            {
                tmProText.text = "Sound: ON";
            }
            else
            {
                tmProText.text = "Sound: OFF";
            }
        }
        else if (legacyText != null)
        {
            // If legacy UI Text is found, update its text.
            if (AudioListener.volume > 0.0f)
            {
                legacyText.text = "Sound: ON";
            }
            else
            {
                legacyText.text = "Sound: OFF";
            }
        }
    }

    // Good practice: Remove listeners when the GameObject is destroyed.
    void OnDestroy()
    {
        if (soundToggleButton != null)
        {
            soundToggleButton.onClick.RemoveListener(ToggleSound);
        }
    }
}

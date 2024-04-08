using UnityEngine;
using UnityEngine.UI; // For UI elements
using UnityEngine.Audio; // For audio management
using TMPro;


public class OptionsMenuManager : MonoBehaviour
{
    public GameObject optionsMenuPanel;
    public Toggle backgroundMusicToggle;
    public TMP_Dropdown resolutionDropdown;
    public AudioSource backgroundMusic;

    private void Start()
    {
        optionsMenuPanel.SetActive(false); // Ensure options menu is hidden at start
        backgroundMusicToggle.gameObject.SetActive(false);
        resolutionDropdown.gameObject.SetActive(false);

        // Initialize dropdown options if needed
        resolutionDropdown.options.Clear();
        resolutionDropdown.options.Add(new TMP_Dropdown.OptionData("640x480"));
        resolutionDropdown.options.Add(new TMP_Dropdown.OptionData("800x600"));
        resolutionDropdown.options.Add(new TMP_Dropdown.OptionData("1024x768"));
        resolutionDropdown.options.Add(new TMP_Dropdown.OptionData("1280x800"));
        resolutionDropdown.options.Add(new TMP_Dropdown.OptionData("1366x768"));
        resolutionDropdown.options.Add(new TMP_Dropdown.OptionData("1920x1200"));
        resolutionDropdown.options.Add(new TMP_Dropdown.OptionData("2560x1440"));    
        resolutionDropdown.options.Add(new TMP_Dropdown.OptionData("3840x2160"));
        
        resolutionDropdown.RefreshShownValue();
    }

    public void ToggleOptionsMenu(bool isOpen)
    {
        optionsMenuPanel.SetActive(isOpen);
        if (isOpen)
        {
            // Show resolution options and background music toggle when opening the options menu
            ShowResolutionOptions();
            backgroundMusicToggle.gameObject.SetActive(true);
        }
        else
        {
            // Hide them again when closing the menu
            resolutionDropdown.gameObject.SetActive(false);
            backgroundMusicToggle.gameObject.SetActive(false);
        }
    }


    public void ToggleBackgroundMusic()
    {
        if (backgroundMusicToggle.isOn)
        {
            backgroundMusic.Play();
        }
        else
        {
            backgroundMusic.Stop();
        }
    }

    public void ShowResolutionOptions()
    {
        resolutionDropdown.gameObject.SetActive(true);
    }
    public void ChangeResolution(int index)
    {
        Debug.Log("ChangeResolution called with index: " + index);
        string[] resolution = resolutionDropdown.options[index].text.Split('x');
        Debug.Log("Setting resolution to: " + resolution[0] + "x" + resolution[1]);
        Screen.SetResolution(int.Parse(resolution[0]), int.Parse(resolution[1]), Screen.fullScreen);
    }


    public void BackToMainMenu()
    {
        // Here, save any settings changes as needed
        optionsMenuPanel.SetActive(false);
        // Reset sub-options visibility
        backgroundMusicToggle.gameObject.SetActive(false);
        resolutionDropdown.gameObject.SetActive(false);
    }
}

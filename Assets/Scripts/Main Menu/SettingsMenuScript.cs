using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuScript : MonoBehaviour
{
    [SerializeField]
    private Button ExitSettingButton;

    [SerializeField]
    private GameObject MainMenuPanel;

    [SerializeField]
    private GameObject SettingsPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ExitSettingButton.onClick.AddListener(ExitSettings_Click);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ExitSettings_Click()
    {
        MainMenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }
}

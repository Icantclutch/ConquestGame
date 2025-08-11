using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    private Button NewCampaignButton;

    [SerializeField]
    private Button LoadCampaignButton;

    [SerializeField]
    private Button SettingsButton;

    [SerializeField]
    private Button ExitButton;


    [SerializeField]
    private GameObject MainMenuPanel;

    [SerializeField]
    private GameObject SettingsPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NewCampaignButton.onClick.AddListener(NewCampaginButton_Click);
        LoadCampaignButton.onClick.AddListener(LoadCampaignButton_Click);
        SettingsButton.onClick.AddListener(SettingsButton_Click);   
        ExitButton.onClick.AddListener(ExitButton_Click);   

        //Set the correct active panels
        MainMenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void NewCampaginButton_Click()
    {

    }

    private void LoadCampaignButton_Click()
    {

    }

    private void SettingsButton_Click()
    {
        MainMenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    private void ExitButton_Click()
    {
        //Debug.Log("Exit Application");
        Application.Quit();
    }
}

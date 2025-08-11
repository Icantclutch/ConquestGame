using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;

public class NewCampaignScript : MonoBehaviour
{
    [SerializeField]
    private Button StartCampaignButton;

    [SerializeField]
    private Button BackButton;

    [SerializeField]
    private GameObject MainMenuPanel;

    [SerializeField]
    private GameObject NewCampaignSettingsPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BackButton.onClick.AddListener(ReturnToMainMenu_Click);
        StartCampaignButton.onClick.AddListener(StartCampaign_Click);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ReturnToMainMenu_Click()
    {
        NewCampaignSettingsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    private void StartCampaign_Click()
    {
        SceneManager.LoadScene("TacticalMap");
    }
}

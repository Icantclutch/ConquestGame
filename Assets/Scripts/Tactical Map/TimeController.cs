using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public int GLOBAL_TIME_SCALE;

    [SerializeField]
    private Button PauseButton, OneXButton, TwoXButton, ThreeXButton;


    public Action GenerateResources;


    [SerializeField]
    private float ResourceTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GLOBAL_TIME_SCALE = 0;
        ResourceTimer = 3;

        PauseButton.onClick.AddListener(PauseButton_Pressed);
        OneXButton.onClick.AddListener(OneXButton_Pressed);
        TwoXButton.onClick.AddListener(TwoXButton_Pressed);
        ThreeXButton.onClick.AddListener(ThreeXButton_Pressed);

        PauseButton.interactable = false;

    } 

    // Update is called once per frame
    void Update()
    {
        if(ResourceTimer <= 0)
        {
            GenerateResources.Invoke();
            ResourceTimer = 3;
        }

        ResourceTimer -= Time.deltaTime * GLOBAL_TIME_SCALE;
    }

    private void PauseButton_Pressed()
    {
        GLOBAL_TIME_SCALE = 0;
        PauseButton.interactable = false;
        OneXButton.interactable = true;
        TwoXButton.interactable = true;
        ThreeXButton.interactable = true;        
    }

    private void OneXButton_Pressed()
    {
        GLOBAL_TIME_SCALE = 1;
        PauseButton.interactable = true;
        OneXButton.interactable = false;
        TwoXButton.interactable = true;
        ThreeXButton.interactable = true;
    }

    private void TwoXButton_Pressed()
    {
        GLOBAL_TIME_SCALE = 2;
        PauseButton.interactable = true;
        OneXButton.interactable = true;
        TwoXButton.interactable = false;
        ThreeXButton.interactable = true;
    }

    private void ThreeXButton_Pressed()
    {
        GLOBAL_TIME_SCALE = 3;
        PauseButton.interactable = true;
        OneXButton.interactable = true;
        TwoXButton.interactable = true;
        ThreeXButton.interactable = false;
    }
}

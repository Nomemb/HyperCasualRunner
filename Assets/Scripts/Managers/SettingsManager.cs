using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private SoundsManager soundsManager;
    [SerializeField] private VibrationManager vibrationManager;
    [SerializeField] private Sprite optionsOnSprite;
    [SerializeField] private Sprite optionsOffSprite;
    [SerializeField] private Image soundsButtonImage;
    [SerializeField] private Image hapticsButtonImage;

    [Header(" Settings ")]
    private bool soundState = true;
    private bool hapticsState = true;

    private void Awake()
    {
        // 플레이어 설정 가져옴
        soundState = PlayerPrefs.GetInt("sounds", 1) == 1;
        hapticsState = PlayerPrefs.GetInt("haptics", 1) == 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Setup()
    {
        if (soundState)
            EnableSounds();
        else
            DisableSounds();
        
        if (hapticsState)
            EnableHaptics();
        else
            DisableHaptics();
    }
    public void ChangeSoundsState()
    {
        if (soundState)
            DisableSounds();
        else
            EnableSounds();

        soundState = !soundState;
        
        // 플레이어 설정 저장함
        PlayerPrefs.SetInt("sounds", soundState? 1:0);
    }

    private void EnableSounds()
    {
        soundsManager.EnableSounds();
        soundsButtonImage.sprite = optionsOnSprite;
    }
    
    private void DisableSounds()
    {
        soundsManager.DisableSounds();
        soundsButtonImage.sprite = optionsOffSprite;
    }


    public void ChangeHapticState()
    {
        if (hapticsState)
            DisableHaptics();
        else
            EnableHaptics();

        hapticsState = !hapticsState;  
        PlayerPrefs.SetInt("haptics", hapticsState? 1:0);

    }
    
    private void EnableHaptics()
    {
        vibrationManager.EnableVibrations();
        hapticsButtonImage.sprite = optionsOnSprite;
    }

    private void DisableHaptics()
    {
        vibrationManager.DisableVibrations();
        hapticsButtonImage.sprite = optionsOffSprite;
    }
}

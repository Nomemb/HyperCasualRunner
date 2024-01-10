using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private SoundsManager soundsManager;
    [SerializeField] private Sprite optionsOnSprite;
    [SerializeField] private Sprite optionsOffSprite;
    [SerializeField] private Image soundsButtonImage;
    [SerializeField] private Image hapticsButtonImage;

    [Header(" Settings ")]
    private bool soundState = true;
    private bool hapticsState = true;
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
    }
    public void ChangeSoundsState()
    {
        if (soundState)
            DisableSounds();
        else
            EnableSounds();

        soundState = !soundState;
    }

    private void DisableSounds()
    {
        soundsManager.DisableSounds();
        soundsButtonImage.sprite = optionsOffSprite;
    }

    private void EnableSounds()
    {
        soundsManager.EnableSounds();
        soundsButtonImage.sprite = optionsOnSprite;

    }
}

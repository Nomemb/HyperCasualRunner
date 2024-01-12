using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShopManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Button purchaseButton;
    [SerializeField] private SkinButton[] skinButtons;
    
    [Header(" Skins ")]
    [SerializeField] private Sprite[] skins;

    [Header(" Pricing ")]
    [SerializeField] private int skinPrice;
    [SerializeField] private TextMeshProUGUI priceText;

    private void Awake()
    {
        priceText.text = skinPrice.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        ConfigureButtons();
        
        UpdatePurchaseButton();
    }

    // Update is called once per frame
    void Update()
    {
        // 디버깅용
        if(Input.GetKeyDown(KeyCode.Space))
            UnlockSkin(Random.Range(0, skinButtons.Length));
        
        // 디버깅용
        if(Input.GetKeyDown(KeyCode.D))
            PlayerPrefs.DeleteAll();
    }

    private void ConfigureButtons()
    {
        for (int i = 0; i < skinButtons.Length; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("skinButton" + i) == 1;
            skinButtons[i].Configure(skins[i], unlocked);

            int skinIndex = i;
            skinButtons[i].GetButton().onClick.AddListener(() => SelectSkin(skinIndex));
        }
    }

    public void UnlockSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("skinButton" + skinIndex, 1);
        skinButtons[skinIndex].Unlock();
    }

    private void UnlockSkin(SkinButton skinButton)
    {
        // Skin Grid 자식오브젝트의 몇번째 인덱스인지 가져옴
        int skinIndex = skinButton.transform.GetSiblingIndex();
        UnlockSkin(skinIndex);
    }

    private void SelectSkin(int skinIndex)
    {
        int length = skinButtons.Length;
        for (int i = 0; i < length; i++)
        {
            if (skinIndex == i)
                skinButtons[i].Select();
            else
                skinButtons[i].Deselect();
        }
    }

    private void SelectSkin(SkinButton skinButton)
    {
        int skinIndex = skinButton.transform.GetSiblingIndex();
        SelectSkin(skinIndex);
    }

    public void PurchaseSkin()
    {
        List<SkinButton> skinButtonsList = new List<SkinButton>();

        int length = skinButtons.Length;
        // 해금되지 않은 스킨만 리스트에 추가.
        for (int i = 0; i < length; i++)
        {
            if (!skinButtons[i].IsUnlocked())
            {
                skinButtonsList.Add(skinButtons[i]);
            }
        }

        if (skinButtonsList.Count <= 0)
            return;

        SkinButton randomSkinButton = skinButtonsList[Random.Range(0, skinButtonsList.Count)];
        
        UnlockSkin(randomSkinButton);
        SelectSkin(randomSkinButton);

        DataManager.instance.UseCoins(skinPrice);
        
        UpdatePurchaseButton();
    }

    public void UpdatePurchaseButton()
    {
        if (DataManager.instance.GetCoins() < skinPrice)
            purchaseButton.interactable = false;
        else
            purchaseButton.interactable = true;

    }
}

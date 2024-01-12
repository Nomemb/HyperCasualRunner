using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    
    [Header(" Coin Texts")]
    [SerializeField] private TextMeshProUGUI[] coinTexts;
    private int coins;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
        
        coins = PlayerPrefs.GetInt("coins", 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateCoinsTexts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateCoinsTexts()
    {
        foreach (TextMeshProUGUI coinText in coinTexts)
        {
            coinText.text = coins.ToString();
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        
        UpdateCoinsTexts();
        
        PlayerPrefs.SetInt("coins", coins);
    }
}

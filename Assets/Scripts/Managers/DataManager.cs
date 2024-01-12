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
    [SerializeField] private int hasCoin;
    private int coins;

    [Header(" Events")]
    public static Action onAddCoins;

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
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameManager.GameState gameState)
    {
        if(gameState == GameManager.GameState.GameOver)
            ResetCoins();
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
        onAddCoins?.Invoke();
        PlayerPrefs.SetInt("coins", coins);
    }

    public void ResetCoins()
    {
        PlayerPrefs.SetInt("hasCoins", coins);
        Debug.Log(PlayerPrefs.GetInt("hasCoins"));
        coins = 0;
        UpdateCoinsTexts();
        PlayerPrefs.SetInt("coins", coins);
    }
}

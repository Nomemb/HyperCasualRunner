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
        // 디버깅용
        AddCoins(5);
        
        
        UpdateCoinsTexts();
        
        
        // GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        // GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    // 매 판 코인을 계속 들고가는 시스템이라면 주석 처리
    // private void GameStateChangedCallback(GameManager.GameState gameState)
    // {
    //     if(gameState == GameManager.GameState.GameOver)
    //         ResetCoins();
    // }

    private void UpdateCoinsTexts()
    {
        foreach (TextMeshProUGUI coinText in coinTexts)
        {
            coinText.text = coins.ToString();
        }
    }

    public void UseCoins(int amount)
    {
        coins -= amount;
        UpdateCoinsTexts();
        PlayerPrefs.SetInt("coins", coins);
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        
        UpdateCoinsTexts();
        onAddCoins?.Invoke();
        PlayerPrefs.SetInt("coins", coins);
    }

    // public void ResetCoins()
    // {
    //     PlayerPrefs.SetInt("hasCoins", coins);
    //     coins = 0;
    //     UpdateCoinsTexts();
    //     PlayerPrefs.SetInt("coins", coins);
    // }

    public int GetCoins()
    {
        return PlayerPrefs.GetInt("hasCoins", coins);
    }
}

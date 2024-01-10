using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerDetection.onDoorHit += ()=>Vibration.Vibrate(800);
        Enemy.onRunnerDied += () => Vibration.Vibrate(800);
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        PlayerDetection.onDoorHit -= ()=>Vibration.Vibrate(800);
        Enemy.onRunnerDied -= () => Vibration.Vibrate(800);
        GameManager.onGameStateChanged -= GameStateChangedCallback;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void GameStateChangedCallback(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.LevelComplete)
            Vibration.Vibrate(800);
        else if (gameState == GameManager.GameState.GameOver)
            Vibration.Vibrate(800);
        
    }
}

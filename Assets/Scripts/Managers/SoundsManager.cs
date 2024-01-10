using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [Header(" Sounds ")]
    [SerializeField] private AudioSource doorHitSound;
    [SerializeField] private AudioSource runnerDie;
    [SerializeField] private AudioSource levelCompleteSound;
    [SerializeField] private AudioSource gameOverSound;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerDetection.onDoorHit += PlayDoorHitSound;

        GameManager.onGameStateChanged += GameStateChangedCallback;
        Enemy.onRunnerDied += PlayRunnerDieSound;
    }

    private void OnDestroy()
    {
        PlayerDetection.onDoorHit -= PlayDoorHitSound;
        
        GameManager.onGameStateChanged -= GameStateChangedCallback;
        Enemy.onRunnerDied -= PlayRunnerDieSound;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GameStateChangedCallback(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.LevelComplete)
            levelCompleteSound.Play();
        else if (gameState == GameManager.GameState.GameOver)
            gameOverSound.Play();
        
    }
    private void PlayDoorHitSound()
    {
        doorHitSound.Play();
    }

    private void PlayRunnerDieSound()
    {
        runnerDie.Play();
    }
}

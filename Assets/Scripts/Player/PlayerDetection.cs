using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerDetection : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private CrowdSystem crowdSystem;

    [Header(" Events ")]
    public static Action onDoorHit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.IsGameState())
            DetectColliders();
    }

    private void DetectColliders()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, crowdSystem.GetCrowdRadius() + 2);

        for (int i = 0; i < detectedColliders.Length; i++)
        {
            if (detectedColliders[i].TryGetComponent(out Doors doors))
            {
                int bonusAmount = doors.GetBonusAmount(transform.position.x);
                BonusType bonusType = doors.GetBonusType(transform.position.x);

                doors.Disable();
                
                onDoorHit?.Invoke();
                
                crowdSystem.ApplyBonus(bonusType, bonusAmount);
            }

            else if (detectedColliders[i].CompareTag("Finish"))
            {
                PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
                GameManager.instance.SetGameState(GameManager.GameState.LevelComplete);
                
            }
            
            else if (detectedColliders[i].CompareTag("Coin"))
            {
                Destroy(detectedColliders[i].gameObject);
                
                DataManager.instance.AddCoins(1);
            }
        }
    }
}

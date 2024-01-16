using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class CrowdSystem : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private Transform runnersParent;
    [SerializeField] private GameObject runnerPrefab;
    [SerializeField] private int initCrowdAmount;
    [SerializeField] private int curCrowdAmount;

    public int CurCrowdAmount => curCrowdAmount;
    private IObjectPool<Runner> pool;

    [Header(" Settings ")]
    [SerializeField] private float radius;
    [SerializeField] private float angle;

    private void Awake()
    {
        pool = new ObjectPool<Runner>(AddRunner, OnGetRunner, OnReleaseRunner, OnDestroyRunner, maxSize:100);
        
    }

    private void Start()
    {
        AddRunners(initCrowdAmount);
        PlaceRunners();
        playerAnimator.Idle();
        curCrowdAmount = initCrowdAmount;
        Enemy.onRunnerDied += ReduceRunnerCount;
    }

    private void OnDestroy()
    {
        Enemy.onRunnerDied -= ReduceRunnerCount;

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.IsGameState())
            return;
        
        PlaceRunners();
        
        if(runnersParent.childCount <= 0)
            GameManager.instance.SetGameState(GameManager.GameState.GameOver);
    }

    private void PlaceRunners()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Vector3 childLocalPosition = GetRunnerLocalPosition(i);
            runnersParent.GetChild(i).localPosition = childLocalPosition;
        }
    }

    private Vector3 GetRunnerLocalPosition(int index)
    {
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angle);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angle);

        return new Vector3(x, 0, z);
    }

    public float GetCrowdRadius()
    {
        return radius * Mathf.Sqrt(runnersParent.childCount);
    }

    public void ApplyBonus(BonusType bonusType, int bonusAmount)
    {
        switch(bonusType)
        {
            case BonusType.Addition:
                AddRunners(bonusAmount);
                break;

            case BonusType.Product:
                int runnersToAdd = runnersParent.childCount * (bonusAmount-1);
                AddRunners(runnersToAdd);
                break;

            case BonusType.Difference:
                RemoveRunners(bonusAmount);
                break;

            case BonusType.Division:
                int runnersToRemove = runnersParent.childCount - runnersParent.childCount / bonusAmount;
                RemoveRunners(runnersToRemove);
                break;

        }
    }

    private void AddRunners(int amount)
    {
        curCrowdAmount += amount;
        for (int i = 0; i < amount; i++)
        {
            // Instantiate(runnerPrefab, runnersParent);
            Runner runner = pool.Get();
            runner.transform.SetParent(runnersParent);
        }
        
        playerAnimator.Run();

    }
    private void RemoveRunners(int amount)
    {
        if (amount > runnersParent.childCount)
            amount = runnersParent.childCount;

        int runnersAmount = curCrowdAmount;

        // 오브젝트 풀링 미사용 코드
        // for (int i = runnersAmount - 1; i >= runnersAmount - amount; i--)
        // {
        //     Transform runnerToDestroy = runnersParent.GetChild(i);
        //     runnerToDestroy.SetParent(null);
        //     Destroy(runnerToDestroy.gameObject);
        // }
        
        for (int i = runnersAmount - 1; i >= runnersAmount - amount; i--)
        {
            Runner runnerToDestroy = runnersParent.GetChild(i).GetComponent<Runner>();
            pool.Release(runnerToDestroy);
            
        }

        curCrowdAmount = runnersAmount - amount;
    }

    private Runner AddRunner()
    {
        Runner runner = Instantiate(runnerPrefab).GetComponent<Runner>();
        runner.SetManagedPool(pool);
        return runner;
    }

    private void OnGetRunner(Runner runner)
    {
        runner.gameObject.SetActive(true);
    }
    
    private void OnReleaseRunner(Runner runner)
    {
        runner.gameObject.SetActive(false);
    }

    private void OnDestroyRunner(Runner runner)
    {
        Destroy(runner.gameObject);
    }

    private void ReduceRunnerCount()
    {
        curCrowdAmount -= 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Runner : MonoBehaviour
{
    [Header(" Settings ")]
    private bool isTarget;
    private IObjectPool<Runner> managedPool;

    public void SetTarget()
    {
        isTarget = true;
    }
    public bool IsTarget()
    {
        return isTarget;
    }

    public void SetManagedPool(IObjectPool<Runner> pool)
    {
        managedPool = pool;
    }

    public void DestroyRunner()
    {
        managedPool.Release(this);
    }
}

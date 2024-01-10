using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform runnersParent;
    
    public void Run()
    {
        int runnerCount = runnersParent.childCount;
        for (int i = 0; i < runnerCount; i++)
        {
            Transform runner = runnersParent.GetChild(i);
            Animator runnerAnimator = runner.GetComponent<Animator>();
            
            runnerAnimator.Play("Run");
        }
    }
    
    public void Idle()
    {
        int runnerCount = runnersParent.childCount;
        for (int i = 0; i < runnerCount; i++)
        {
            Transform runner = runnersParent.GetChild(i);
            Animator runnerAnimator = runner.GetComponent<Animator>();
            
            runnerAnimator.Play("Idle");
        }
    }
}

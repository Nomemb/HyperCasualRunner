using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrowdCounter : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshPro crowdCounterText;
    [SerializeField] private CrowdSystem crowdSystem;

    // Update is called once per frame
    void Update()
    {
        int childCount = crowdSystem.CurCrowdAmount;
        crowdCounterText.text = childCount.ToString();

        if (childCount <= 0)
            Destroy(gameObject);
    }
}

using TMPro;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshPro crowdCounterText;
    [SerializeField] private Transform enemyParent;

    // Update is called once per frame
    void Update()
    {
        int enemyCount = enemyParent.childCount;
        crowdCounterText.text = enemyCount.ToString();

        if (enemyCount <= 0)
            Destroy(gameObject);
    }
}
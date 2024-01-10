using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum BonusType { Addition, Difference, Product, Division }
public class Doors : MonoBehaviour
{

    [Header(" Elements ")]
    [SerializeField] private MeshRenderer leftDoorRenderer;
    [SerializeField] private TextMeshPro leftDoorText;
    [SerializeField] private MeshRenderer rightDoorRenderer;
    [SerializeField] private TextMeshPro rightDoorText;
    [SerializeField] private Collider collider;

    [Header(" Settings ")]
    [SerializeField] private BonusType leftDoorBonusType;
    [SerializeField] private int leftDoorBonusAmount;

    [SerializeField] private BonusType rightDoorBonusType;
    [SerializeField] private int rightDoorBonusAmount;


    [SerializeField] private Material bounsMaterial;
    [SerializeField] private Material penaltyMaterial;



    // Start is called before the first frame update
    void Start()
    {
        ConfigureDoors();
    }

    private void ConfigureDoors() 
    {
        rightDoorRenderer.material = (rightDoorBonusType == BonusType.Addition) ? bounsMaterial : penaltyMaterial;
        rightDoorText.text = ((rightDoorBonusType == BonusType.Addition) ? "+" : "-") + rightDoorBonusAmount;

        leftDoorRenderer.material = (leftDoorBonusType == BonusType.Addition) ? bounsMaterial : penaltyMaterial;
        leftDoorText.text = ((leftDoorBonusType == BonusType.Addition) ? "+" : "-") + leftDoorBonusAmount;

        switch (rightDoorBonusType)
        {
            case BonusType.Addition:
                rightDoorRenderer.material = bounsMaterial;
                rightDoorText.text = "+" + rightDoorBonusAmount;
                break;

            case BonusType.Difference:
                rightDoorRenderer.material = penaltyMaterial;
                rightDoorText.text = "-" + rightDoorBonusAmount;
                break;

            case BonusType.Product:
                rightDoorRenderer.material = bounsMaterial;
                rightDoorText.text = "X" + rightDoorBonusAmount;
                break;
            case BonusType.Division:
                rightDoorRenderer.material = penaltyMaterial;
                rightDoorText.text = "/" + rightDoorBonusAmount;
                break;

        }

        switch (leftDoorBonusType)
        {
            case BonusType.Addition:
                leftDoorRenderer.material = bounsMaterial;
                leftDoorText.text = "+" + leftDoorBonusAmount;
                break;

            case BonusType.Difference:
                leftDoorRenderer.material = penaltyMaterial;
                leftDoorText.text = "-" + leftDoorBonusAmount;
                break;

            case BonusType.Product:
                leftDoorRenderer.material = bounsMaterial;
                leftDoorText.text = "X" + leftDoorBonusAmount;
                break;
            case BonusType.Division:
                leftDoorRenderer.material = penaltyMaterial;
                leftDoorText.text = "/" + leftDoorBonusAmount;
                break;

        }
    }

    public int GetBonusAmount(float xPosition)
    {
        if (xPosition > 0)
            return rightDoorBonusAmount;
        else
            return leftDoorBonusAmount;
    }

    public BonusType GetBonusType(float xPosition)
    {
        if (xPosition > 0)
            return rightDoorBonusType;
        else
            return leftDoorBonusType;
    }

    public void Disable()
    {
        collider.enabled = false;
    }
}

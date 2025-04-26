using Assets.Scripts.Plants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Plant : MonoBehaviour
{
    // Definition of the plant.
    public PlantDefinition definition;
    public SpriteRenderer spriteRenderer;
    // The cells this plant occupies.
    public List<Vector3Int> plantCells = new List<Vector3Int>();

    // How long this plant has been growing.
    public float currentGrowthTime = 0f;
    // 0 = N / 1 = E / 2 = S / 3 = W
    public int currentRotation = 0;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Returns whether or not this plant is done growing.
    public bool IsDoneGrowing() => currentGrowthTime >= definition.timeToGrow;

    public void Rotate()
    {
        currentRotation = (currentRotation + 1) % 4;
        spriteRenderer.sprite = definition.sprites[currentRotation];
    }


    void setPlantCells(Vector3Int origin)
    {
        Vector3Int direction;

        switch (currentRotation)
        {
            case 0:
                direction = new Vector3Int(0, 1, 0);
                break;
            case 1:
                direction = new Vector3Int(1, 0, 0);
                break;
            case 2:
                direction = new Vector3Int(0, -1, 0);
                break;
            case 3:
                direction = new Vector3Int(-1, 0, 0);
                break;
            default:
                direction = new Vector3Int(0, 1, 0);
                break;
        }

        switch (definition.plantShape)
        {
            case "1x1":
                plantCells.Add(origin);
                break;
            case "1x2":
                plantCells.Add(origin);
                plantCells.Add(origin + direction);
                break;
            case "1x3":
                plantCells.Add(origin);
                plantCells.Add(origin + direction);
                plantCells.Add(origin + direction * 2);
                break;
            case "L":
                plantCells.Add(origin);
                plantCells.Add(origin + direction);
                plantCells.Add(new Vector3Int(origin.x + direction.y, origin.y + direction.x, 0));
                break;
            case "2x2":
                plantCells.Add(origin);
                plantCells.Add(origin + direction);
                plantCells.Add(new Vector3Int(origin.x + direction.y, origin.y + direction.x, 0));
                plantCells.Add(new Vector3Int(origin.x + direction.y + direction.x, origin.y + direction.x + direction.y, 0));
                break; 

        }
    }

}

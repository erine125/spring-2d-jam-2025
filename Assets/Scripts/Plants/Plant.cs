using Assets.Scripts.Plants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Plant : MonoBehaviour
{
    public enum PlantRotation
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
    }

    // Definition of the plant.
    public PlantDefinition definition;
    public SpriteRenderer spriteRenderer;
    // The cells this plant occupies.
    public List<Vector3Int> plantCells = new List<Vector3Int>();

    // How long this plant has been growing.
    public float currentGrowthTime = 0f;
    // 0 = N / 1 = E / 2 = S / 3 = W
    public PlantRotation currentRotation = PlantRotation.North;

    private GameObject slider_;


    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Returns whether or not this plant is done growing.
    public bool IsDoneGrowing() => currentGrowthTime >= definition.timeToGrow;

    public void CreateTimerUI()
    {
        Debug.Log("Now creating timer UI");
        GameObject timerPrefab = definition.timerPrefab;
        slider_ = Instantiate(timerPrefab, transform.position + new Vector3(0.5f, 1.2f, 0), Quaternion.identity);
        Debug.Log("Instantiated "+ slider_.name);
        slider_.transform.SetParent(GameObject.Find("Canvas").transform);
        slider_.GetComponent<Slider>().value = 0f;
        
    }

    public void UpdatePlantTimerUI()
    {
        slider_.GetComponent<Slider>().value = currentGrowthTime / definition.timeToGrow;
    }

    public void Rotate()
    {
        switch(currentRotation)
        {
            case PlantRotation.North:
                currentRotation = PlantRotation.East;
                break;
            case PlantRotation.East:
                currentRotation = PlantRotation.South;
                break;
            case PlantRotation.South:
                currentRotation = PlantRotation.West;
                break;
            case PlantRotation.West:
                currentRotation = PlantRotation.North;
                break;
        }
        spriteRenderer.sprite = definition.sprites[(int)currentRotation];
    }

    // Called when sheared; for most Plants, reset its growth.
    public virtual void Shear()
    {
        currentGrowthTime = 0f;
        spriteRenderer.sprite = definition.sprites[(int)currentRotation];
    }

    public void SetPlantCells(Vector3Int origin)
    {
        plantCells.Clear();
        Vector3Int direction;

        switch (currentRotation)
        {
            case PlantRotation.North:
                direction = new Vector3Int(0, 1, 0);
                break;
            case PlantRotation.East:
                direction = new Vector3Int(1, 0, 0);
                break;
            case PlantRotation.South:
                direction = new Vector3Int(0, -1, 0);
                break;
            case PlantRotation.West:
                direction = new Vector3Int(-1, 0, 0);
                break;
            default:
                direction = new Vector3Int(0, 1, 0);
                break;
        }

        switch (definition.plantShape)
        {
            case PlantShape._1x1:
                plantCells.Add(origin);
                break;
            case PlantShape._1x2:
                plantCells.Add(origin);
                plantCells.Add(origin + direction);
                break;
            case PlantShape._1x3:
                plantCells.Add(origin);
                plantCells.Add(origin + direction);
                plantCells.Add(origin + direction * 2);
                break;
            case PlantShape._L:
                plantCells.Add(origin);
                plantCells.Add(origin + direction);
                plantCells.Add(origin + new Vector3Int(direction.y, -direction.x, 0));
                break;
            case PlantShape._2x2:
                plantCells.Add(origin); // br
                plantCells.Add(origin - new Vector3Int(1, 0, 0)); // bl
                plantCells.Add(new Vector3Int(origin.x, origin.y + 1, 0)); // tr
                plantCells.Add(new Vector3Int(origin.x - 1, origin.y + 1, 0)); // tl
                break;
        }

        //// Debug placed positions
        //var text = Enum.GetName(typeof(PlantRotation), currentRotation) + " | ";
        //foreach (Vector3Int v in plantCells) text += v.x + "," + v.y + " | ";
        //Debug.Log(text);
    }
}

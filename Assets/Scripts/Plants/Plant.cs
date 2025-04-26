using Assets.Scripts.Plants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    // Definition of the plant.
    [SerializeField] private PlantDefinition definition;

    public SpriteRenderer spriteRenderer;

    // How long this plant has been growing.
    public float currentGrowthTime = 0f;
    public Vector3Int cellPosition;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Returns whether or not this plant is done growing.
    public bool IsDoneGrowing() => currentGrowthTime >= definition.timeToGrow;

    public List<Vector2Int> GetRotatedCells()
    {
        var cells = new List<Vector2Int>();
        var rotation = (spriteRenderer.transform.eulerAngles.z % 360f) / 90f;
        if (rotation == 0) return cells;

        foreach (Vector2Int cellPosition in definition.cells)
        {
            
            switch (rotation)
            {
                case 0: // 0 degrees; already handled
                    break;
                case 1: // 90 degrees

                    break;
                case 2: // 180
                    break;
                case 3: // 270
                    break;
                default: // ???
                    break;
            }
        }
        
        return cells;
    }

    public void PlaceAt(Vector3Int cellPos, float rotation)
    {
        cellPosition = cellPos;
    }

}

using Assets.Scripts;
using Assets.Scripts.Plants;
using System;
using UnityEngine;

public class ShearTool : AGardenTool
{
    [SerializeField] private PlantDefinition shearDefinition;
    [SerializeField] private Plant previewPlant;

    public override Vector2[] GetCells()
    {
        throw new NotImplementedException();
    }

    public override void Rotate()
    {
        previewPlant.Rotate();
    }

    public override void UpdatePreview(Vector3Int previousCellPosition, Vector3Int newCellPosition)
    {
        previewPlant.transform.position = interactiveMap.CellToWorld(newCellPosition);
    }

    public override bool Use(Vector3Int cellPos)
    {
        // TODO: get plants at rotated cells, and operate on them appropriately

        return false;
    }

    public override void SetActive()
    {
        previewPlant.definition = shearDefinition;
        previewPlant.currentRotation = Plant.PlantRotation.North;
        previewPlant.spriteRenderer.sprite = shearDefinition.sprites[0];
    }
}


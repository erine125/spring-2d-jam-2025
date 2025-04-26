using Assets.Scripts;
using Assets.Scripts.Plants;
using System;
using UnityEngine;

public class ShearTool : AGardenTool
{
    [SerializeField] private Color invalidColor = new Color(0.5f, 0, 0, 0.5f);
    [SerializeField] private GardenManager GardenManager;
    [SerializeField] private PlantDefinition shearDefinition;
    [SerializeField] private Plant previewPlant;

    public override Vector2[] GetCells()
    {
        return new[] { new Vector2(0, 0), new Vector2(0, -1) };
    }

    public override void Rotate()
    {
        previewPlant.Rotate();
    }

    public override void UpdatePreview(Vector3Int previousCellPosition, Vector3Int newCellPosition)
    {
        previewPlant.transform.position = interactiveMap.CellToWorld(newCellPosition);

        previewPlant.SetPlantCells(newCellPosition);
        foreach (Vector3Int cell in previewPlant.plantCells)
        {
            if (!GardenManager.SoilMap.HasTile(cell))
            {
                previewPlant.spriteRenderer.color = invalidColor;
                return;
            }
        }
        previewPlant.spriteRenderer.color = Color.white;
    }

    public override bool Use(Vector3Int cellPos)
    {
        // TODO: get plants at rotated cells, and operate on them appropriately

        previewPlant.SetPlantCells(cellPos);
        foreach (Vector3Int cell in previewPlant.plantCells)
        {
            if (!GardenManager.SoilMap.HasTile(cell)) return false; // Within the playspace
            // If there's a plant, then shear it.
            if (GardenManager.TryGetPlant(cell, out Plant plant))
            {
                // TODO: weed shearing animation/polish
                // Remove from map if it's a weed
                if (plant is WeedPlant) GardenManager.RemovePlantFromTile(cell);
                // Shear the plant
                plant.Shear();
            }
            
        }

        return true;
    }

    public override void SetActive()
    {
        // TODO: set shear cursor icon

        previewPlant.definition = shearDefinition;
        previewPlant.currentRotation = Plant.PlantRotation.North;
        previewPlant.spriteRenderer.sprite = shearDefinition.sprites[0];
        previewPlant.spriteRenderer.color = Color.white; // reset color from previous tool
    }
}


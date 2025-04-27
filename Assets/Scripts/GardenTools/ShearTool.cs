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
    [SerializeField] private GridManager GridManager_;

    public AudioSource audioSource;
    public AudioClip shearSFX;
    public AudioClip selectToolSFX;

    public ButtonManager buttonManager;
    public int buttonIdx;

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
        previewPlant.spriteRenderer.color = new(1, 1, 1, 0.5f);
    }

    public override bool Use(Vector3Int cellPos)
    {
        previewPlant.SetPlantCells(cellPos);
        foreach (Vector3Int cell in previewPlant.plantCells)
        {
            if (!GardenManager.SoilMap.HasTile(cell)) return false; // Within the playspace
        }

        foreach (Vector3Int cell in previewPlant.plantCells)
        {
            // If there's a plant, then shear it.
            if (GardenManager.TryGetPlant(cell, out Plant plant))
            {
                // TODO: weed shearing animation/polish

                // Play shearing sound 
                audioSource.PlayOneShot(shearSFX, 0.9f);

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

        buttonManager.setButtonIcon(buttonIdx);

        // Play select tool sound
        audioSource.PlayOneShot(selectToolSFX);

        previewPlant.definition = shearDefinition;
        previewPlant.currentRotation = Plant.PlantRotation.North;
        previewPlant.spriteRenderer.sprite = shearDefinition.sprites[0];
        previewPlant.spriteRenderer.color = new(1, 1, 1, 0.5f); // reset color from previous tool

        GridManager_.toolIdx = 6;
        GridManager_.tool = this;
    }
}


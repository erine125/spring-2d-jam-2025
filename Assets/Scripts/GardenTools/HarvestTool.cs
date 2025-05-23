using Assets.Scripts;
using Assets.Scripts.Plants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestTool : AGardenTool
{
    public GardenManager GardenManager;
    
    [SerializeField] private Plant previewPlant;
    [SerializeField] private PlantDefinition harvestDefinition;
    [SerializeField] private GridManager GridManager_;

    public AudioClip selectToolSFX;
    public AudioClip harvestSFX;

    public AudioSource audioSource;
    public ButtonManager buttonManager;
    public int buttonIdx;

    public override Vector2[] GetCells()
    {
        throw new System.NotImplementedException();
    }

    // Harvesting doesn't need to rotate
    public override void Rotate() { }

    public override void UpdatePreview(Vector3Int previousCellPosition, Vector3Int newCellPosition)
    {
        previewPlant.transform.position = interactiveMap.CellToWorld(newCellPosition);

        if (GardenManager.TryGetPlant(newCellPosition, out Plant plant))
        {
            if (plant is WeedPlant)
            {
                previewPlant.spriteRenderer.sprite = null;
            }
            else if (plant.IsDoneGrowing())
            {
                previewPlant.spriteRenderer.sprite = harvestDefinition.sprites[0];
            } else
            {
                previewPlant.spriteRenderer.sprite = harvestDefinition.grownSprites[0];
            }
        } else
        {
            previewPlant.spriteRenderer.sprite = null;
        }
    }

    public override bool Use(Vector3Int cellPos)
    {
        if (GardenManager.TryGetPlant(cellPos, out Plant plant))
        {
            if (plant.IsDoneGrowing())
            {
                GardenManager.HarvestPlant(plant);
                audioSource.PlayOneShot(harvestSFX, 1.1f);
                return true;
            }
        }
        return false;
    }

    public override void SetActive()
    {
        // TODO: set harvest/basket cursor icon

        buttonManager.setButtonIcon(buttonIdx);

        // Play select tool sound
        audioSource.PlayOneShot(selectToolSFX);

        previewPlant.definition = harvestDefinition;
        previewPlant.currentRotation = Plant.PlantRotation.North;
        previewPlant.spriteRenderer.sprite = null;
        previewPlant.spriteRenderer.color = Color.white; // Reset color


        GridManager_.toolIdx = 0;
        GridManager_.tool = this;
    }
}

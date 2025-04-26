using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GardenManager : MonoBehaviour
{
    List<Plant> livePlants = new();
    Dictionary<Vector3Int, Plant> plantMap = new();

    [SerializeField] private Tilemap soilMap;
    public GridManager gridManager;

    public bool CanPlace(Plant previewPlant)
    {
        previewPlant.SetPlantCells(gridManager.GetMouseCellPosition());

        foreach (Vector3Int cell in previewPlant.plantCells)
        {
            if (!soilMap.HasTile(cell)) return false; // Within the playspace
            if (plantMap.ContainsKey(cell)) return false; // Doesn't have a plant in it
        }
        return true;
    }

    public void AddPlant(Plant plant)
    {
        if (CanPlace(plant))
        {
            foreach (Vector3Int cell in plant.plantCells)
            {
                plantMap.Add(cell, plant);
            }
            livePlants.Add(plant);

            Debug.Log("Added plant to garden at: " + plant.plantCells[0].x + ", " + plant.plantCells[0].y);
        }
    }

}

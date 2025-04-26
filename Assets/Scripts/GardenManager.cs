using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GardenManager : MonoBehaviour
{
    List<Plant> livePlants = new();
    Dictionary<Vector3Int, Plant> plantMap = new();
    
    // Duration of the run
    public float gameTimer = 60f * 3f;
    public Tilemap SoilMap;
    public GridManager GridManager;
    public OrderQueue orderQueue;

    // Returns true if there is a plant at this position else false.
    // The plant at the position is received via `out Plant plant`
    public bool TryGetPlant(Vector3Int cellPos, out Plant plant) => plantMap.TryGetValue(cellPos, out plant);
    
    public bool CanPlace(Plant previewPlant)
    {
        previewPlant.SetPlantCells(GridManager.GetMouseCellPosition());

        foreach (Vector3Int cell in previewPlant.plantCells)
        {
            if (!SoilMap.HasTile(cell)) return false; // Within the playspace
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

    /*
     * This is the core game loop of the game, handling time, plant growth, and orders incoming.
    */
    void Update()
    {
        HandleTimer();
        GrowPlants();
    }

    void HandleTimer()
    {
        gameTimer -= Time.deltaTime;
        // TODO: display the updated gameTimer to the canvas

        // TODO: grow weeds

    }

    // Tick every plant every frame.
    void GrowPlants()
    {
        foreach (Plant plant in livePlants) 
            if (!plant.IsDoneGrowing())
               plant.currentGrowthTime += Time.deltaTime;
    }
}

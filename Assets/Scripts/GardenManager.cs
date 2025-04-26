using Assets.Scripts.Plants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class GardenManager : MonoBehaviour
{
    List<Plant> livePlants = new();
    Dictionary<Vector3Int, Plant> plantMap = new();

    // Duration of the run
    public float gameTimer = 180f;
    // Weeds spawn every this many seconds
    public float timeBetweenWeeds = 20f;

    public Tilemap SoilMap;
    public GridManager GridManager;
    public OrderQueue OrderQueue;

    [SerializeField] private PlantDefinition weedDefinition;
    [SerializeField] private GameObject plantHolder;

    private float weedTimer = 0.0f;

    public bool HasPlant(Vector3Int cellPos) => plantMap.ContainsKey(cellPos);

    // Returns true if there is a plant at this position else false.
    // The plant at the position is received via `out Plant plant`
    public bool TryGetPlant(Vector3Int cellPos, out Plant plant) => plantMap.TryGetValue(cellPos, out plant);
    
    public bool CanPlace(Plant previewPlant)
    {
        previewPlant.SetPlantCells(GridManager.GetMouseCellPosition());

        foreach (Vector3Int cell in previewPlant.plantCells)
        {
            if (!SoilMap.HasTile(cell)) return false; // Within the playspace
            if (HasPlant(cell)) return false; // Doesn't have a plant in it
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

    // Remove a plant from the plant map.
    public bool RemovePlantFromTile(Vector3Int cellPos)
    {
        return plantMap.Remove(cellPos);
    }
    
    // Return a list of all available tiles in the tilemap that don't have a plant in them.
    List<Vector3Int> GetAllOpenTiles(Tilemap tilemap)
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        List<Vector3Int> openTiles = new();
        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    //Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                    var cellPos = new Vector3Int(x, y, 0);
                    if (HasPlant(cellPos)) openTiles.Add(cellPos);
                }
            }
        }

        return openTiles;
    }

    // Called by HarvestTool on a valid plant.
    // Sends it to OrderQueue to fulfill orders.
    public void HarvestPlant(Plant plant)
    {
        // Trim from map
        foreach (var cellPos in plant.plantCells) RemovePlantFromTile(cellPos);

        // TODO: OrderQueue
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
        // TODO: display the updated gameTimer to the canvas
        gameTimer -= Time.deltaTime;

        // Place weeds every X seconds
        weedTimer += Time.deltaTime;
        if (weedTimer >= timeBetweenWeeds)
        {
            weedTimer = 0f;
            PlaceWeed();
        }
    }

    // Tick every plant every frame.
    void GrowPlants()
    {
        // Only grow non-weeds
        foreach (Plant plant in livePlants)
            if (plant is not WeedPlant && !plant.IsDoneGrowing())
               plant.currentGrowthTime += Time.deltaTime;
    }

    // Place a weed at the appropriate location.
    GameObject PlaceWeed()
    {
        // No tiles to place a weed in
        var openTiles = GetAllOpenTiles(SoilMap);
        if (openTiles.Count == 0) return null;

        var randomRotation = (Plant.PlantRotation)Random.Range(0, Enum.GetValues(typeof(Plant.PlantRotation)).Length);
        var cellPos = openTiles[Random.Range(0, openTiles.Count)];

        GameObject o = new("Weed");
        o.transform.SetParent(plantHolder.transform);
        o.transform.localScale = plantHolder.transform.localScale;
        o.transform.SetPositionAndRotation(SoilMap.CellToWorld(cellPos), o.transform.rotation);

        var plant = o.AddComponent<WeedPlant>();
        plant.definition = weedDefinition;
        plant.currentRotation = randomRotation;
        plant.plantCells = new() { cellPos };

        var sr = o.AddComponent<SpriteRenderer>();
        sr.sprite = weedDefinition.sprites[(int)randomRotation];
        sr.sortingLayerName = "Grid";
        sr.sortingOrder = 1;

        // Add to map for tracking
        plantMap.Add(cellPos, plant);

        return o;
    }

}

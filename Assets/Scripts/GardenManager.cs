using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenManager : MonoBehaviour
{
    List<Plant> livePlants = new();
    Dictionary<Vector3Int, Plant> plantMap = new();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool canPlace(Plant previewPlant)
    {
        foreach (Vector3Int cell in previewPlant.plantCells)
        {
            if (plantMap.ContainsKey(cell))
            {
                return false;
            }
        }
        return true;
    }

    public void addPlant(Plant plant)
    {
        if (canPlace(plant))
        {
            foreach (Vector3Int cell in plant.plantCells)
            {
                plantMap.Add(cell, plant);
            }
            livePlants.Add(plant);
        }
    }

}

using Assets.Scripts.Plants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Assets.Scripts.GardenTools
{
    public class SeedTool : AGardenTool
    {
        [SerializeField] private PlantDefinition plantDefinition;
        [SerializeField] private Plant previewPlant;
        [SerializeField] private GameObject plantHolder;

        private Vector2[] tiles;

        public GardenManager gardenManager;

        public void SetCells(Vector2[] cells) => tiles = cells;
        public override Vector2[] GetCells() => tiles;

        public override void UpdatePreview(Vector3Int previousCellPosition, Vector3Int newCellPosition)
        {
            previewPlant.transform.position = interactiveMap.CellToWorld(newCellPosition);
            //Debug.Log(newCellPosition);
        }

        public override void Rotate()
        {
            previewPlant.Rotate();
        }

        public override bool Use(Vector3Int cellPos)
        {
            // check that it's not overlapping with an existing plant or weed
            if (gardenManager.CanPlace(previewPlant))
            {
                GameObject newPlant = PlacePlantSprite();
                var plant = newPlant.GetComponent<Plant>();
                plant.SetPlantCells(cellPos);
                gardenManager.AddPlant(plant);

                previewPlant.plantCells.Clear();
                return true;
            }
            else
            {
                Debug.Log("Cannot place plant here.");
                return false;
            }
        }

        // Places the plant into actual play space.
        GameObject PlacePlantSprite()
        {
            GameObject o = new("Plant");
            o.transform.SetParent(plantHolder.transform);
            o.transform.localScale = plantHolder.transform.localScale;
            o.transform.SetPositionAndRotation(previewPlant.transform.position, o.transform.rotation);

            var plant = o.AddComponent<Plant>();
            plant.definition = previewPlant.definition;
            plant.currentRotation = previewPlant.currentRotation;
            // Copy the cells since otherwise it's passed by reference
            foreach (var cell in previewPlant.plantCells)
                plant.plantCells.Add(new Vector3Int(cell.x, cell.y, 0));

            plant.CreateTimerUI();

            var sr = o.AddComponent<SpriteRenderer>();
            sr.sprite = previewPlant.spriteRenderer.sprite;
            sr.sortingLayerName = "Grid";
            sr.sortingOrder = 1;

            return o;
        }

        public override void SetActive()
        {
            // TODO: set seed cursor icon

            previewPlant.definition = plantDefinition;
            previewPlant.currentRotation = Plant.PlantRotation.North;
            previewPlant.spriteRenderer.sprite = plantDefinition.sprites[0];
        }
    }
}

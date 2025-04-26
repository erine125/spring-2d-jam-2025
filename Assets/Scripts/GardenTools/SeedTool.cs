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
        private Vector2[] tiles;

        public GardenManager gardenManager;

        public void SetCells(Vector2[] cells) => tiles = cells;
        public override Vector2[] GetCells() => tiles;

        public override void UpdatePreview(Vector3Int previousCellPosition, Vector3Int newCellPosition)
        {
            previewPlant.transform.position = interactiveMap.CellToWorld(newCellPosition);
        }

        public override void Rotate()
        {
            previewPlant.Rotate();
        }

        public override bool Use(Vector3Int cellPos)
        {
            // TODO: check that it's not overlapping with an existing plant or weed

            // place plant on grid

            
            return true;
        }

        public override void SetActive()
        {
            previewPlant.definition = plantDefinition;
            previewPlant.currentRotation = 0;
            previewPlant.spriteRenderer.sprite = plantDefinition.sprites[0];
        }
    }
}

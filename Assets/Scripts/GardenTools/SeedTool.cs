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
        [SerializeField] private List<PlantDefinition> plantDefinitions;
        [SerializeField] private Plant previewPlant;
        private Vector2[] tiles;

        public GardenManager gardenManager;

        int idx = 0;

        public void SetCells(Vector2[] cells) => tiles = cells;
        public override Vector2[] GetCells() => tiles;

        public override void UpdatePreview(Vector3Int previousCellPosition, Vector3Int newCellPosition)
        {
            // Set sprite
            previewPlant.spriteRenderer.sprite = plantDefinitions[idx].sprite[0];
            previewPlant.spriteRenderer.transform.position = interactiveMap.CellToWorld(newCellPosition);
        }

        public override void Rotate()
        {
            if (!plantDefinitions[idx].canBeRotated) return;
            previewPlant.spriteRenderer.transform.Rotate(0, 0, 90);
        }

        public override bool Use(Vector3Int cellPos)
        {

            // TODO: check that it's not overlapping with an existing plant or weed

            // place plant on grid

            idx = (idx + 1) % plantDefinitions.Count;
            previewPlant.spriteRenderer.sprite = plantDefinitions[idx].sprite[0];
            return true;
        }

    }
}

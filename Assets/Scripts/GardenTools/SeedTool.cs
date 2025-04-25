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
        private Vector2[] tiles;
        [SerializeField] private Tilemap plantMap;
        [SerializeField] private Tilemap interactiveMap;

        public GardenManager gardenManager;

        public void SetCells(Vector2[] cells) => tiles = cells;
        public override Vector2[] GetCells() => tiles;

        public override void UpdatePreview(Vector3Int previousPosition, Vector3Int newPosition)
        {
            throw new NotImplementedException();
        }
        public override void Rotate()
        {
            throw new NotImplementedException();
        }
        public override bool Use(Vector3Int cellPos)
        {

            // TODO: check that it's not overlapping with an existing plant or weed

            // place plant on grid



            throw new NotImplementedException();
        }

    }
}

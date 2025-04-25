using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GardenTools
{
    public class SeedTool : AGardenTool
    {
        private Vector2[] tiles;

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
        public override bool Use()
        {
            throw new NotImplementedException();
        }

    }
}

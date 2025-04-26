using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Plants
{
    [CreateAssetMenu(fileName = "Data", menuName = "PlantDefinition", order = 1)]
    public class PlantDefinition : ScriptableObject
    {
        // Name of the plant.
        public string plantName;
        // Sprite visual for the plant when placed.
        public List<Sprite> sprite;
        // What platonic cells this plant uses.
        public List<Vector2Int> cells;
        // Whether it can be rotated to begin with.
        public bool canBeRotated;
        // How long this plant takes to grow in seconds.
        public float timeToGrow = 10f;
    }
}

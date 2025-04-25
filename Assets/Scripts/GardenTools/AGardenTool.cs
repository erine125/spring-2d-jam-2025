using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts
{
    public abstract class AGardenTool : MonoBehaviour
    {
        [SerializeField] private Tilemap plantMap;
        [SerializeField] private Tilemap previewMap;

        // Returns the platonic cells of the tool ( [(0,0), (1, 0), (0, 1)] )
        public abstract Vector2[] GetCells();


        public abstract void UpdatePreview(Vector3Int previousPosition, Vector3Int newPosition);

        // Called to attempt to rotate the tool clockwise.
        public abstract void Rotate();

        // Called to use the tool.
        // Returns true if the operation succeeded and false otherwise.
        public abstract bool Use();
    }
}
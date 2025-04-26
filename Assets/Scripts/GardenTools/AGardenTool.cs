using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts
{
    public abstract class AGardenTool : MonoBehaviour
    {
        [SerializeField] protected Tilemap interactiveMap;

        // Returns the platonic cells of the tool ( [(0,0), (1, 0), (0, 1)] )
        public abstract Vector2[] GetCells();


        public abstract void UpdatePreview(Vector3Int previousCellPosition, Vector3Int newCellPosition);

        // Called to attempt to rotate the tool clockwise.
        public abstract void Rotate();

        // Called to use the tool.
        // Returns true if the operation succeeded and false otherwise.
        public abstract bool Use(Vector3Int cellPos);
        // Called when this tool is set as active.
        public virtual void SetActive() { }
    }
}
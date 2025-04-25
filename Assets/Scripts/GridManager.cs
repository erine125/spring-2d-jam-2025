using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GridManager : MonoBehaviour

{

    private Grid grid;
    [SerializeField] private Tilemap interactiveMap;
    [SerializeField] private Tilemap dirtMap;
    [SerializeField] private Tilemap plantMap;
    [SerializeField] private Tilemap seedMap;
    [SerializeField] private Tile hoverTile;
    [SerializeField] private Tile dirtTile;
    [SerializeField] private Tile plant1;
    [SerializeField] private Tile plant2;

    private Tile plantTile;

    private Vector3Int previousMousePos = new Vector3Int();

    // Start is called before the first frame update
    void Start()
    {
        grid = gameObject.GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse over -> highlight tile
        Vector3Int mousePos = GetMousePosition();
        if (!mousePos.Equals(previousMousePos))
        {
            interactiveMap.SetTile(previousMousePos, null); // Remove old hoverTile
            if (dirtMap.HasTile(mousePos))
            {
                interactiveMap.SetTile(mousePos, hoverTile);
            }
            previousMousePos = mousePos;
        }

        // Left mouse click -> add path tile
        if (Input.GetMouseButton(0))
        {
            if (seedMap.GetTile(mousePos) == plant1)
            {
                plantTile = plant1;
            }
            else if (seedMap.GetTile(mousePos) == plant2)
            {
                plantTile = plant2;
            }

            if (dirtMap.GetTile(mousePos) == dirtTile)
            {
                plantMap.SetTile(mousePos, plantTile);
            }
        }

        // Right mouse click -> remove path tile
        if (Input.GetMouseButton(1))
        {
            plantMap.SetTile(mousePos, null);
        }
    }

    Vector3Int GetMousePosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }
}

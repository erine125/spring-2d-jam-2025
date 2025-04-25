using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Tilemap interactiveMap;
    [SerializeField] private Tilemap dirtMap;
    [SerializeField] private Tilemap plantMap;
    [SerializeField] private Tile hoverTile;

    [SerializeField] private GameObject toolObject;

    private AGardenTool[] toolList;

    private Grid grid;
    private AGardenTool tool;

    private Vector3Int previousMousePos = new();

    // Start is called before the first frame update
    void Start()
    {
        grid = gameObject.GetComponent<Grid>();
        toolList = toolObject.GetComponents<AGardenTool>();
        tool = toolList[0];
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse over -> highlight tile
        Vector3Int mousePos = GetMousePosition();
        if (!mousePos.Equals(previousMousePos))
        {
            if (tool) tool.UpdatePreview(previousMousePos, mousePos);
            else interactiveMap.SetTile(previousMousePos, null); // Remove old hoverTile

            previousMousePos = mousePos;
            previousMousePos = mousePos;
        }

        // Left mouse click -> add path tile
        if (Input.GetMouseButton(0))
        {
            if (tool) tool.Use();
        }

        // Right mouse click -> remove path tile
        if (Input.GetMouseButton(1))
        {
            if (tool) tool.Rotate();
        }
    }

    Vector3Int GetMousePosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }
}

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

    private Vector3Int previousMouseCellPos = new();

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
        Vector3Int mouseCellPos = GetMouseCellPosition();
        if (!mouseCellPos.Equals(previousMouseCellPos))
        {
            if (tool) tool.UpdatePreview(previousMouseCellPos, mouseCellPos);
            else interactiveMap.SetTile(previousMouseCellPos, null); // Remove old hoverTile

            previousMouseCellPos = mouseCellPos;
        }

        // Left mouse click -> use tool
        if (Input.GetMouseButtonDown(0))
        {
            if (dirtMap.HasTile(mouseCellPos)){ // check that it's a valid space
                if (tool) tool.Use(mouseCellPos);
            }
        }

        // Right mouse click -> remove path tile
        if (Input.GetMouseButtonDown(1))
        {
            if (tool) tool.Rotate();
        }
    }



    Vector3Int GetMouseCellPosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }
}

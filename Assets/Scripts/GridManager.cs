using Assets.Scripts;
using System;
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
    public int toolIdx = 0;
    public AGardenTool tool;

    private Vector3Int previousMouseCellPos = new();

    // Start is called before the first frame update
    void Start()
    {
        grid = gameObject.GetComponent<Grid>();
        toolList = toolObject.GetComponents<AGardenTool>();

        toolIdx = 0;
        tool = toolList[toolIdx];
        tool.SetActive();
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse over -> highlight tile
        Vector3Int mouseCellPos = GetMouseCellPosition();
        if (tool) tool.UpdatePreview(previousMouseCellPos, mouseCellPos);
        previousMouseCellPos = mouseCellPos;
        //if (!mouseCellPos.Equals(previousMouseCellPos))
        //{
        //    if (tool) tool.UpdatePreview(previousMouseCellPos, mouseCellPos);
        //    else interactiveMap.SetTile(previousMouseCellPos, null); // Remove old hoverTile

        //    previousMouseCellPos = mouseCellPos;
        //}

        // Left mouse click -> use tool
        if (Input.GetMouseButtonDown(0))
        {
            if (dirtMap.HasTile(mouseCellPos))
            { // check that it's a valid space
                if (tool) tool.Use(mouseCellPos);
            }
        }
        // Right mouse click -> rotate plant
        else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.R))
        {
            if (tool) tool.Rotate();
        }
        // Scroll to cycle tools
        else if (Input.mouseScrollDelta.y > 0)
        {
            toolIdx = (toolIdx - 1) % toolList.Length;
            if (toolIdx < 0) toolIdx = toolList.Length - 1;
            tool = toolList[toolIdx];
            tool.SetActive();
        } else if (Input.mouseScrollDelta.y < 0)
        {
            toolIdx = (toolIdx + 1) % toolList.Length;
            tool = toolList[toolIdx];
            tool.SetActive();
        }
    }

    public Vector3Int GetMouseCellPosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }
}

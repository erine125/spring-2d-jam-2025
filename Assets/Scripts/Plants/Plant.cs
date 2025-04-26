using Assets.Scripts.Plants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    // Definition of the plant.
    [SerializeField] public PlantDefinition definition;

    public SpriteRenderer spriteRenderer;

    // How long this plant has been growing.
    public float currentGrowthTime = 0f;
    // 0 = N / 1 = E / 2 = S / 3 = W
    public int currentRotation = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Returns whether or not this plant is done growing.
    public bool IsDoneGrowing() => currentGrowthTime >= definition.timeToGrow;

    public void Rotate()
    {
        currentRotation = (currentRotation + 1) % 4;
        spriteRenderer.sprite = definition.sprites[currentRotation];
    }
}

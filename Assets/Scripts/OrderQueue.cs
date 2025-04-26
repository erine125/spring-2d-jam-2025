using UnityEngine;
using Assets.Scripts.Plants;
using System.Collections.Generic;

public class OrderQueue : MonoBehaviour
{
    public PlantDefinition[] PossiblePlants;
    public int QueueLimit = 4;
    public float OrderFrequencyMin = 3f;
    public float OrderFrequencyMax = 5f;

    // Time until next plant
    private double countdown;
    public double GetCountdown()
    {
        return countdown;
    }

    // Not a true queue as only first matching element will be popped, not first element
    private List<PlantDefinition> queue;
    public int GetQueueLength()
    {
        return queue.Count;
    }
    public PlantDefinition GetQueueAt(int index)
    {
        if (queue.Count > index) return queue[index];
        else return null;
    }


    public OrderQueue()
    {
        queue = new List<PlantDefinition>();
        countdown = 0.01;
    }

    void Update()
    {
        if (countdown > 0) countdown -= Time.deltaTime;
        if (countdown <= 0) AddOrder();
    }


    // Returns true if the order matched
    public bool FillOrder(string plantName)
    {
        int found = -1;
        for (int i = 0; i < queue.Count; ++i)
        {
            if (queue[i].plantName == plantName)
            {
                found = i;
                break;
            }
        }

        if (found != -1)
        {
            queue.RemoveAt(found);
            return true;
        }

        return false;
    }

    private bool AddOrder()
    {
        if (GetQueueLength() > QueueLimit) return false;

        countdown = 0.01f;

        PlantDefinition plant = PossiblePlants[Random.Range(0, PossiblePlants.Length - 1)];
        queue.Add(plant);
        countdown = Random.Range(OrderFrequencyMin, OrderFrequencyMax);

        var text = "Orders: ";
        foreach (var item in queue) text += $"{item.plantName}, ";
        Debug.Log(text);

        return true;
    }
}
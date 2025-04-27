using UnityEngine;
using Assets.Scripts.Plants;
using System.Collections.Generic;
using TMPro;

public class OrderQueue : MonoBehaviour
{
    public static int OrdersCompleted = 0;

    public List<TextMeshProUGUI> OrderLabels;

    public PlantDefinition[] PossiblePlants;
    public int QueueLimit = 4;
    public float OrderFrequencyMin = 3f;
    public float OrderFrequencyMax = 6f;

    [SerializeField][Multiline] private List<string> npcDialogues = new();

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
        if (countdown <= 0)
        {
            var plantName = AddOrder();
            if (plantName != "") // if order was added
            {
                // TODO: update order ui
                foreach (var label in OrderLabels)
                {
                    if (label != null && label.text == "")
                    {
                        // NPC dialogue
                        var text = npcDialogues[Random.Range(0, npcDialogues.Count)];
                        string dialogue = text.Replace("{Plant}", $"<color=\"red\">{plantName}</color>");
                        label.text = dialogue;
                        break;
                    }
                }
            }
        }
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
            // TODO: display score
            OrdersCompleted += 1;

            OrderLabels[found].text = "";
            queue.RemoveAt(found);
            return true;
        }

        return false;
    }

    private string AddOrder()
    {
        if (GetQueueLength() > QueueLimit) return "";

        PlantDefinition plant = PossiblePlants[Random.Range(0, PossiblePlants.Length - 1)];
        queue.Add(plant);
        countdown = Random.Range(OrderFrequencyMin, OrderFrequencyMax);

        var text = "Orders: ";
        foreach (var item in queue) text += $"{item.plantName}, ";
        Debug.Log(text);

        return plant.plantName;
    }
}
using UnityEngine;
using Assets.Scripts.Plants;
using System.Collections.Generic;
using TMPro;

public class OrderQueue : MonoBehaviour
{
    public static int OrdersCompleted = 0;

    public Canvas ParentCanvas;
    public GameObject CardPrefab;

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
    private List<Order> queue;
    public int GetQueueLength()
    {
        return queue.Count;
    }
    public Order GetQueueAt(int index)
    {
        if (queue.Count > index) return queue[index];
        else return null;
    }

    public OrderQueue()
    {
        queue = new List<Order>();
        countdown = 0.01;
    }

    void Update()
    {
        if (countdown > 0) countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            var plantName = AddOrder();
        }

        foreach (Order order in queue)
        {
            order.Update ();
        }
    }

    // Returns true if the order matched
    public bool FillOrder(string plantName)
    {
        int found = -1;
        for (int i = 0; i < queue.Count; ++i)
        {
            if (found != -1)
            {
                queue[i].index -= 1; // decrease the index
            }
            else if (queue[i].plantDef.plantName == plantName)
            {
                found = i;
            }
        }

        if (found != -1)
        {
            // TODO: display score
            OrdersCompleted += 1;

            Destroy (queue[found].obj);
            queue.RemoveAt(found);
            return true;
        }

        return false;
    }

    private string AddOrder()
    {
        if (GetQueueLength() >= QueueLimit) return "";

        PlantDefinition plant = PossiblePlants[Random.Range(0, PossiblePlants.Length - 1)];
        string npcText = npcDialogues[Random.Range(0, npcDialogues.Count)];
        string dialogue = npcText.Replace("{Plant}", $"<color=\"red\">{plant.plantName}</color>");
        int index = queue.Count;
        GameObject obj = Instantiate (CardPrefab, Vector3.zero, Quaternion.identity);
        obj.transform.SetParent (ParentCanvas.transform, false);

        queue.Add(new Order(plant, dialogue, index, obj));
        countdown = Random.Range(OrderFrequencyMin, OrderFrequencyMax);

        var dbgText = "Orders: ";
        foreach (var item in queue) dbgText += $"{item.plantDef.plantName}, ";
        Debug.Log(dbgText);

        return plant.plantName;
    }
}

public class Order
{
    public PlantDefinition plantDef;
    public string text;
    public int index;
    public GameObject obj;
    public RectTransform pos;

    private float CARD_Y_OFFSET = -70; // offset between each card slot
    private float OFFSCREEN_X = 275;
    private float SPEED_X = 700;
    private float SPEED_Y = 500;

    public Order (PlantDefinition plantDef, string text, int index, GameObject obj)
    {
        this.plantDef = plantDef;
        this.index = index;
        this.obj = obj;
        pos = obj.GetComponent<RectTransform> ();
        obj.GetComponentInChildren<TextMeshProUGUI> ().text = text;

        UpdatePos (OFFSCREEN_X, CARD_Y_OFFSET * index);
    }

    public void Update ()
    {
        Vector2 target = GetTargetPos ();
        Vector2 cur = new Vector2 (pos.anchoredPosition.x, pos.anchoredPosition.y);
        Vector2 result = Vector2.zero;
        float dx = target.x - cur.x;
        float dy = target.y - cur.y;

        float xSpd = SPEED_X * Mathf.Min (1, 0.2f * Mathf.Log (Mathf.Abs(dx) + 4)); // easing func
        if (Mathf.Abs(dx) <= Time.deltaTime * xSpd) result.x = target.x;
        else result.x = cur.x + Mathf.Sign (dx) * xSpd * Time.deltaTime;

        float ySpd = SPEED_Y * Mathf.Min (1, 0.2f * Mathf.Log (Mathf.Abs(dy) + 4)); // easing func
        if (Mathf.Abs(dy) <= Time.deltaTime * ySpd) result.y = target.y;
        else result.y = cur.y + Mathf.Sign (dy) * ySpd * Time.deltaTime;

        UpdatePos (result.x, result.y);
    }

    private Vector2 GetTargetPos ()
    {
        Vector2 v = Vector2.zero;
        v.x = 0;
        v.y = CARD_Y_OFFSET * index;
        return v;
    }

    private void UpdatePos (float x, float y)
    {
        this.pos.anchoredPosition = new Vector2 (x, y);
    }
}
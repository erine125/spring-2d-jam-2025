using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreTracker : MonoBehaviour
{
    public GameObject scoreText;
    // Start is called before the first frame update
    void Start()
    { 
        int score;
        score = OrderQueue.OrdersCompleted;
        Debug.Log(score);
        scoreText.GetComponent<TextMeshProUGUI>().text = "What a busy season! You fulfilled " + score.ToString() + " orders!";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

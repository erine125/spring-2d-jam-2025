using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    private const float timeToWait = 1f;
    private float timerProgress = 0f;

    private void Start()
    {
        timerProgress = 0f;
    }

    private void Update()
    {
        if (timerProgress < timeToWait) timerProgress += Time.deltaTime;
    }

    private void OnMouseDown()
    {
        if (timerProgress < timeToWait) return;

        OrderQueue.OrdersCompleted = 0;
        SceneManager.LoadScene("MainGame");
        //SceneManager.LoadScene("TitleScreen");
    }
}

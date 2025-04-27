using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    private void OnMouseDown()
    {
        OrderQueue.OrdersCompleted = 0;
        //SceneManager.LoadScene("MainGame");
        SceneManager.LoadScene("TitleScreen");
    }
}

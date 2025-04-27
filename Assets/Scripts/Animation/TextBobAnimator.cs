using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBobAnimator : MonoBehaviour
{
    public float Amplitude = 4f;
    public float Frequency = 1f;

    private float timer = 0f;
    private float offset = 0;
    private RectTransform rt;
    private int initHeight;

    void Start()
    {
        rt = GetComponent<RectTransform> ();
        initHeight = (int) Mathf.Round (rt.anchoredPosition.y);
    }

    void Update()
    {
        timer += Time.deltaTime;
        offset = Amplitude * Mathf.Sin (Frequency * timer);
        rt.anchoredPosition = new Vector2 (rt.anchoredPosition.x, initHeight + (int) Mathf.Round (offset));
    }
}

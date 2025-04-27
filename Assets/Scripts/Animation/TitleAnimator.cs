using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimator : MonoBehaviour
{
    public float ChangeFreq = 0.25f;
    public Sprite[] Sprites;

    private float timer = 0;
    private int index = 0;

    private SpriteRenderer spriteRenderer;

    void Start ()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > ChangeFreq)
        {
            index = (index + 1) % Sprites.Length;
            timer = 0;
            spriteRenderer.sprite = Sprites[index];
        }
        
    }
}

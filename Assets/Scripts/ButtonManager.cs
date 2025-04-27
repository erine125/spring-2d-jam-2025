using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    public GameObject[] buttons;
    public Sprite[] unselectedIcons;
    public Sprite[] selectedIcons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    } 

    public void setButtonIcon(int idx)
    {
        Debug.Log("Set button icon for: " + idx);
        // reset all icons to unselected version 
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Image>().sprite = unselectedIcons[i];
        }

        // set icon for selected button 
        buttons[idx].GetComponent<Image>().sprite = selectedIcons[idx];

    }
}

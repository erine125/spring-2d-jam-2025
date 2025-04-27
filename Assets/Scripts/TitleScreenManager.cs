using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{

    public bool showedHowToPlay = false;
    public GameObject howToPlayScreen;
    public AudioSource audioSource;
    public AudioClip clickSFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {

        if (!showedHowToPlay)
        {
            showedHowToPlay = true;
            audioSource.PlayOneShot(clickSFX, 1.2f);
            howToPlayScreen.SetActive(true);
            return;
        } else
        {
            audioSource.PlayOneShot(clickSFX, 1.2f);
            SceneManager.LoadScene("MainGame");
        }

            
    }
}

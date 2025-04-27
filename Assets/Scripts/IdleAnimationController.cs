using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnimationController : MonoBehaviour
{

    Animator anim;
    float idleTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        idleTime += Time.deltaTime;

        if (idleTime >= 7f)
        {
            int randchoice = Random.Range(0, 3);

            if (randchoice == 0)
            {
                Debug.Log("Triggering frog animation");
                anim.SetTrigger("frog");
            }
            else if (randchoice == 1)
            {
                Debug.Log("Triggering bunny animation");
                anim.SetTrigger("bunny");
            }
            else
            {
                Debug.Log("Triggering bird animation");
                anim.SetTrigger("bird");
            }
            idleTime = 0f;
        }
    }
}

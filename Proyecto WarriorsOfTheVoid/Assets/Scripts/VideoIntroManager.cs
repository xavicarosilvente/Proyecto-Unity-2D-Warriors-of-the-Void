using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VideoIntroManager : MonoBehaviour
{
    private bool isPlaying = false;
    private float ContVideo;

    public Text PressToSkip;
    private float ContText;

    // Use this for initialization
    void Start()
    {
        isPlaying = true;
        PressToSkip.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {       
        if (isPlaying == true)
        {
            ContVideo += Time.deltaTime;
            if (ContVideo > 22.5f)
            {
                SceneManager.LoadScene(1);
            }

            if (ContVideo > 5)
            {
                ContText += Time.deltaTime;
                if (ContText >= 0 && ContText < 1)
                {
                    PressToSkip.enabled = true;
                }
                else if (ContText > 1 && ContText < 2)
                {
                    PressToSkip.enabled = false;
                }
                else if (ContText > 2)
                {
                    ContText = 0;
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene(1);
                }
            }
        }
    }
}

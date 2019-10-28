using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenuScript : MonoBehaviour
{

    public static AudioMenuScript instance;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
       
    }
}

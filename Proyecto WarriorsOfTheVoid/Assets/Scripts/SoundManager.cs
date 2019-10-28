using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public List<GameObject> SoundEffects;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void CreateSound(int IndexSound)
    {
        GameObject NewSound = Instantiate(SoundEffects[IndexSound]);
        Destroy(NewSound, 3f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateRayos : MonoBehaviour
{

    public Animator Anim;
    public bool Rayos = false;
    public float ContAnim;
    public float ContFalse;

    public LandManager ManagerLand;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Rayos == true)
        {
            ContAnim += Time.deltaTime;
            ContFalse += Time.deltaTime;
            if (ContAnim > 0.3f)
            {
                Anim.SetBool("rayos", true);
                ContAnim = 0;
                if(ContFalse > 0.4f)
                {
                    Anim.SetBool("rayos", false);
                    Destroy(gameObject);
                    Rayos = false;
                }
            }
        }
    }
}

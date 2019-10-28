using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alda : MonoBehaviour
{
    public GameObject Point1, Point2, Point3, Point4;

    // En el start, a través de getComponent accedemos al script CharacterComun para inicializar las variables y adaptarlas
    // al personaje.
    void Start()
    {
        transform.GetComponent<CharacterComun>().Point1 = Point1;
        transform.GetComponent<CharacterComun>().Point2 = Point2;
        transform.GetComponent<CharacterComun>().Point3 = Point3;
        transform.GetComponent<CharacterComun>().Point4 = Point4;
        transform.GetComponent<CharacterComun>().spd = 7;
        transform.GetComponent<CharacterComun>().jumpSpd = 600;
    }

    // Update is called once per frame
    void Update()
    {

    }

    

}

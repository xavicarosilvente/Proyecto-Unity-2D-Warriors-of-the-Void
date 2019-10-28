using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Christian : MonoBehaviour
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
        transform.GetComponent<CharacterComun>().spd = 9;
        transform.GetComponent<CharacterComun>().jumpSpd = 600;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HabilidadEspecial_1()
    {
        if(transform.GetComponent<CharacterComun>().player_1 == true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                // Hacer lo que sea que haga esta habilidad
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                // Hacer lo que sea que haga esta habilidad
            }
        }

    }

}

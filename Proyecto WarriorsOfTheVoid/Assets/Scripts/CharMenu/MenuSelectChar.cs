using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSelectChar : MonoBehaviour
{
    public List<GameObject> tipoPersonajes;
    public GameObject player1, player2;
    public bool seleccionPersonaje1, seleccionandoAlgunPersonaje;
    public CanvasGroup botonesFinalesPersonajes;
    public CanvasGroup escenaSeleccionPersonajes;
    public int indice1, indice2;
    public bool Pj1Terminado, Pj2Terminado;
    public Image ConfirmadoPersonaje1, ConfirmadoPersonaje2; //Imágenes para dejar fijada el "tic" verde y que no se quite al pulsar en otro botón
    public Button BttConfirmPlayer1, BttConfirmPlayer2; //esta variable es para que no me deje pulsar el botón si no he seleccionado un personaje primero.
    public Animator Anim1, Anim2;
    private float Cont2aAnimacion;

    // Use this for initialization
    void Start()
    {
        seleccionPersonaje1 = true;
        seleccionandoAlgunPersonaje = true;
        ConfirmadoPersonaje1.enabled = false;
        ConfirmadoPersonaje2.enabled = false;
        BttConfirmPlayer1.interactable = false;
        BttConfirmPlayer2.interactable = false;
        Pj1Terminado = false;
        Pj2Terminado = false;        
    }

    // Update is called once per frame
    void Update()
    {
        if (Pj1Terminado == true && Pj2Terminado == true)
        {
            SeleccionPersonajesAcabada();
        }
    }

    // función ligada a los botones de selección de personaje
    // maneja la aparición en escena del personaje seleccionado y que guarda en el StaticScript el indice del mismo
    // el parámetro de entrada "indice" indica la posición del personaje en la lista de personajes
    public void SeleccionPersonajes (int indice)
    {
        if (seleccionandoAlgunPersonaje == true)
        {
            if (seleccionPersonaje1 == true)
            {
                if (player1 != null)
                {
                    Destroy(player1);
                }
                GameObject newCharacter = (GameObject)Instantiate(tipoPersonajes[indice], new Vector2(-5.65f, -1f), Quaternion.Euler(0, 0, 0));
                player1 = newCharacter;
                player1.transform.localScale = new Vector3(2, 2, 2);
                player1.GetComponent<CharacterComun>().congelado = true;
                indice1 = indice;
                BttConfirmPlayer1.interactable = true;                
            }
            else
            {
                if (player2 != null)
                {
                    Destroy(player2);
                }
                GameObject newCharacter = (GameObject)Instantiate(tipoPersonajes[indice], new Vector2(5.65f, -1f), Quaternion.Euler(0, 180, 0)); //**RECORDAR CAMBIAR LA Y 180**//
                player2 = newCharacter;
                player2.transform.localScale = new Vector3(2, 2, 2);
                player2.GetComponent<CharacterComun>().congelado = true;
                indice2 = indice;
                BttConfirmPlayer2.interactable = true;
            }
        }        
    }

    // función ligada al botón de confirmación del player 1, que al cambiar la bool hace que los botones de seleccionar personaje
    // pasen a elegir el player 2
    public void ConfirmarPlayer1()
    {        
        if (player1 != null)
        {
            Pj1Terminado = true;
            seleccionPersonaje1 = false;
            ConfirmadoPersonaje1.enabled = true;
            if (player1.name == "Player_1_Alda(Clone)")
            {
                Anim1.SetBool("SelectedAldaP1", true);
            }
            else
            {
                Anim2.SetBool("SelectedChristianP1", true);
            }
        }
        else
        {
            Pj1Terminado = false;
            seleccionPersonaje1 = true;
            ConfirmadoPersonaje1.enabled = false;
            Anim1.SetBool("SelectedAldaP1", false);
            Anim2.SetBool("SelectedChristianP1", false);
        }        
    }

    public void ConfirmarPlayer2()
    {
        if (player2 != null)
        {
            Pj2Terminado = true;
            ConfirmadoPersonaje2.enabled = true;
            if (player2.name == "Player_1_Alda(Clone)" && player1.name == "Player_1_Alda(Clone)")
            {
                Anim1.SetBool("SelectedAldaP1", false);
                Anim1.SetBool("SelectedAldaP2", false);
                Anim1.SetBool("BothSelectedALDA", true);
            }
            else if (player2.name == "Player_1_Sofi(Clone)" && player1.name == "Player_1_Sofi(Clone)")
            {
                Anim2.SetBool("SelectedChristianP1", false);
                Anim2.SetBool("SelectedChristianP2", false);
                Anim2.SetBool("BothSelectedCHRISTIAN", true);
            }
            else if (player2.name == "Player_1_Alda(Clone)" && player1.name == "Player_1_Sofi(Clone)")
            {
                Anim1.SetBool("SelectedAldaP2", true);
            }
            else if (player2.name == "Player_1_Sofi(Clone)" && player1.name == "Player_1_Alda(Clone)")
            {
                Anim2.SetBool("SelectedChristianP2", true);
            }
        }
        else
        {
            Pj2Terminado = false;
            ConfirmadoPersonaje2.enabled = false;
            Anim1.SetBool("SelectedAldaP2", false);
            Anim2.SetBool("SelectedChristianP2", false);
        }
    }

    public void SeleccionPersonajesAcabada()
    {
        botonesFinalesPersonajes.alpha = 1;
        botonesFinalesPersonajes.blocksRaycasts = true;
        botonesFinalesPersonajes.interactable = true;
        seleccionandoAlgunPersonaje = false;
    }


    // función ligada al botón de reelegir personajes.
    // cambia las bool "seleccionandoAlgunPersonaje" y "seleccionPersonaje1" a true para habilitar de nuevo la seleccion de personajes
    // y empezar por el player 1. hace desaparecer los botones finales.
    public void VolverElegirPersonajes()
    {
        botonesFinalesPersonajes.alpha = 0;
        botonesFinalesPersonajes.blocksRaycasts = false;
        botonesFinalesPersonajes.interactable = false;        
        seleccionandoAlgunPersonaje = true;
        seleccionPersonaje1 = true;
        Pj1Terminado = false;
        Pj2Terminado = false;
        ConfirmadoPersonaje1.enabled = false;
        ConfirmadoPersonaje2.enabled = false;
        Anim1.SetBool("SelectedAldaP1", false);
        Anim2.SetBool("SelectedChristianP1", false);
        Anim1.SetBool("SelectedAldaP2", false);
        Anim2.SetBool("SelectedChristianP2", false);
        Anim1.SetBool("BothSelectedALDA", false);
        Anim2.SetBool("BothSelectedCHRISTIAN", false);
        Destroy(player1);
        Destroy(player2);
    }


    // función ligada al botón de ir a elegir escenario.
    // guarda en el StaticScript los indices de los personajes seleccionados de cara a cuando se usen en otras escenas y hace
    // el cambio de escena a la del menú de selección de escenario.
    public void PasarSeleccionEscenario()
    {
        StaticScript.indicePlayer_1 = indice1;
        StaticScript.indicePlayer_2 = indice2;
        StartCoroutine(DelaySceneLoadEscenario());
    }
    IEnumerator DelaySceneLoadEscenario()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(3);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectLandManager : MonoBehaviour
{
    public List<GameObject> tipoPersonajes;
    public GameObject player1, player2, background;
    public int indiceEscenario;

    public List<Sprite> Backgrounds;
    private float CountFill;

    public Button StartFight;

    // Use this for initialization
    void Start()
    {
        indiceEscenario = 0;
        GameObject newCharacter1 = (GameObject)Instantiate(tipoPersonajes[StaticScript.indicePlayer_1], new Vector3(-5.5f, -2.6f, -9.5f), Quaternion.Euler(0, 0, 0));
        player1 = newCharacter1;
        player1.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        player1.GetComponent<CharacterComun>().congelado = true;
        GameObject newCharacter2 = (GameObject)Instantiate(tipoPersonajes[StaticScript.indicePlayer_2], new Vector3(5.5f, -2.6f, -9.5f), Quaternion.Euler(0, 180, 0));
        player2 = newCharacter2;
        player2.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        player2.GetComponent<CharacterComun>().congelado = true;

        background.GetComponent<Image>().color = Color.grey;
        StartFight.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // función ligada a los botones de selección de escenarios.
    // por ahora sólo guarda en el script la escena a la que queremos cambiar.  ***************************
    // el parámetro de entrada "indice" indica el número de la escena a la que se quiere acceder
    public void SeleccionarLand (int indice)
    {
        indiceEscenario = indice;
        StartFight.interactable = true;
        if (indice == 4)
        {
            background.GetComponent<Image>().sprite = Backgrounds[0];
            background.GetComponent<Image>().color = Color.grey;
        }

        if (indice == 5)
        {
            background.GetComponent<Image>().sprite = Backgrounds[1];
            background.GetComponent<Image>().color = Color.grey;
        }
        // lo demás
        // cuando pulsas el botón, el fondo de la escena se cambia por el fondo que habrá en el escenario al que queremos ir
        // y detalles varios que se puedan ocurrir 
    }

    // función ligada al botón de empezar el combate.
    // hace el cambio de escena a la que marque la variable "indiceEscenario", que es el último botón de escenario presionado
    public void CambiarEscena ()
    {
        if (indiceEscenario > 3)
        {
            StartCoroutine(DelaySceneLoadCambiarEscena());
        }

    }

    public void BackToSelectChar()
    {
        StartCoroutine(DelaySceneLoadBackSelectChar());
    }
    IEnumerator DelaySceneLoadBackSelectChar()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(2);

    }
    IEnumerator DelaySceneLoadCambiarEscena()
    {
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene(indiceEscenario);

    }
}

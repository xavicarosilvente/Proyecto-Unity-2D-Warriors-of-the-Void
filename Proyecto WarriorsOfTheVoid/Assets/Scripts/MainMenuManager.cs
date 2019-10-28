using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        StartCoroutine(DelaySceneLoadPlay());
    }

    public void RulesMenu()
    {
        StartCoroutine(DelaySceneLoadRules());

    }

    public void ExitGame()
    {
        StartCoroutine(DelaySceneLoadExit());
    }

    public void CreditsMenu()
    {
        StartCoroutine(DelaySceneLoadCredits());
    }
   
    IEnumerator DelaySceneLoadPlay()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(2);
    }
    IEnumerator DelaySceneLoadExit()
    {
        yield return new WaitForSeconds(0.2f);
        Application.Quit();
    }
    IEnumerator DelaySceneLoadRules()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(6);
        
    }
    IEnumerator DelaySceneLoadCredits()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(7);

    }
}

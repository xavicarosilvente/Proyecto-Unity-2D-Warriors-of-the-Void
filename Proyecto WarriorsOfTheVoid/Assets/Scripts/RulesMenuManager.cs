using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RulesMenuManager : MonoBehaviour
{
    
    public void BackToMainMenu()
    {
        StartCoroutine(DelaySceneLoadBack());
    }
    IEnumerator DelaySceneLoadBack()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(1);
    }
}

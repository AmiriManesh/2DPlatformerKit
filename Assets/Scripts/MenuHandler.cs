using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private string whichScene;
    
    public void LoadScene()
    {
        SceneManager.LoadScene(whichScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

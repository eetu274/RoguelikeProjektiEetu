using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenScript : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Retry()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadFunction : MonoBehaviour
{
    public void loadScene(int sceneindex)
    {
        SceneManager.LoadScene(sceneindex);
    }
    public void exitthegame()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

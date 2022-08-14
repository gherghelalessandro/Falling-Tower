using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadFunction : MonoBehaviour
{
    bool active = true;
    public void loadScene(int sceneindex)
    {
        SceneManager.LoadScene(sceneindex);
    }
    public void pausethegame()
    {
        if(active==true)
        {
            Time.timeScale = 0;
            active = false;
        }
        else
        {
            Time.timeScale = 1;
            active = true;
        }
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

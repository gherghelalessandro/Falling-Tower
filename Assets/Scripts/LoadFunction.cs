using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadFunction : MonoBehaviour
{
    [SerializeField]
    AudioSource music;
    public void loadScene(int sceneindex)
    {
        SceneManager.LoadScene(sceneindex);
    }
    public void pausethegame()
    {
        TextMeshProUGUI text = GameObject.Find("PauseText").GetComponent<TextMeshProUGUI>();
        if(text.text=="")
        {
            Time.timeScale = 0;
            text.text = "Pause";
            music.Pause();
        }
        else
        {
            Time.timeScale = 1;
            text.text = "";
            music.UnPause();
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

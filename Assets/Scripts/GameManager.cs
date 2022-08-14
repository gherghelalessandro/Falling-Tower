using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Spawn spawner;
    [SerializeField]
    eliminations elimination;
    [SerializeField]
    GameOver End;
    [SerializeField]
    TextMeshProUGUI ScoreText;
    [SerializeField]
    TextMeshProUGUI RemainText;
    [SerializeField]
    TextMeshProUGUI HighScoreText;
    [SerializeField]
    SpriteRenderer[] images;
    [SerializeField]
    AudioSource[] audioSources;
    
    string[] divideS, divideR;
    int Score,HighScore;
    int[] timerpow = {15,30,10};
    bool pause = false,gameover=false;
    public bool power1 = false, power2 = false;
    bool[] activepowers = {false,false};

    GameObject playerobj;
    TextMeshProUGUI pausetext;

    void Start()
    {
        divideS = ScoreText.text.Split(":");
        divideR = RemainText.text.Split(":");
        Score = int.Parse(divideS[1]);
        RemainText.text = divideR[0] + ":" + spawner.remains;
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = Color.red;
        }
        StartCoroutine(settrue((x) => power1 = x, 0, timerpow[0]));
        StartCoroutine(settrue((y) => power2 = y, 1, timerpow[1]));
        HighScoreText.text="HighScore:"+PlayerPrefs.GetInt("HighScore",0).ToString();
        audioSources[0].PlayDelayed(4f);
    }
    void Update()
    {
        if (End.gameover==true)
        {
            spawner.gameObject.SetActive(false);
            StartCoroutine(stopthegame(3));
        }
        else
        {
            playerobj = GameObject.FindGameObjectWithTag("tetris");
            if (Input.GetKeyUp(KeyCode.P))
            {

                if (pause == true)
                {
                    pause = false;
                }
                else
                {
                    pause = true;
                }
                stoptheobjects();
            }
            if(Input.GetKeyUp(KeyCode.Z)&&power1==true)
            {
                activepowers[0] = true;
                power1 = false;
                images[0].color = Color.red;
            }
            if(Input.GetKeyDown(KeyCode.X)&&power2==true)
            {
                activepowers[1]=true;
                power2 = false;
                images[1].color = Color.red;
            }
            if (activepowers[0] ==true)
            {
                upthepiece();
                activepowers[0] = false;
            }
            if (activepowers[1] ==true)
            {
                if (playerobj != null)
                {
                    StartCoroutine(littlepiece(timerpow[2], playerobj));
                }
            }
            
        }
    }
    void FixedUpdate()
    {
        if(elimination.destroy==true)
        {
            if(Score>0)
            {
                Score--;
                ScoreText.text = divideS[0] + ":" + Score;
            }
            elimination.destroy = false;
            spawner.touch = false;
        }
        else if(spawner.touch==true)
        {
            Score++;
            if(spawner.remains<0)
            {
                timerpow[timerpow.Length-1]+=5;  
            }
            ScoreText.text = divideS[0] + ":" + Score;
            RemainText.text = divideR[0] + ":" + spawner.remains;
            spawner.touch = false;
        }
    }
    void stoptheobjects()
    {
        pausetext = GameObject.Find("PauseText").GetComponent<TextMeshProUGUI>();
        if (pausetext.text ==string.Empty)
        {
            pausetext.text = "Pause";
            pause = true;
            audioSources[0].Pause();
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            pause = false;
            pausetext.text = "";
            audioSources[0].UnPause();
        }
    }
    void upthepiece()
    {
        playerobj = GameObject.FindGameObjectWithTag("tetris");
        playerobj.transform.position = new Vector3(playerobj.transform.position.x,End.transform.position.y+2);
    }
    void increasethetime()
    {
        for(int i=0;i<timerpow.Length-1;i++)
        {
            timerpow[i] += 40;
        }
    }
    
    IEnumerator settrue(System.Action< bool> active,int index,float timer)
    {
        if(pause==false&&gameover==false)
        {
            yield return new WaitForSecondsRealtime(timer);
            if (activepowers[index] == false&&pause==false&&gameover==false)
            {
                active(true);
                images[index].color = Color.green;
            }
            StartCoroutine(settrue(active, index, timer));
        }
          
    }
   
    IEnumerator stopthegame(float time)
    {
        Destroy(GameObject.FindGameObjectWithTag("tetris"));
        audioSources[0].Stop();
        yield return new WaitForSecondsRealtime(time);
        audioSources[1].Play();
        TextMeshProUGUI GameOverText = GameObject.Find("GameOverText").GetComponent<TextMeshProUGUI>();
        GameOverText.text = "Game Over \nYour score is "+Score;
        if (Score > HighScore)
        {
            HighScore = Score;
            HighScoreText.text = "HighScore:" + HighScore;
            PlayerPrefs.SetInt("HighScore", HighScore);
        }
    }
    IEnumerator littlepiece(int time,GameObject piece)
    {
        piece.transform.localScale = new Vector3(0.6f, 0.6f, 0);
        yield return new WaitForSecondsRealtime(time);
        activepowers[1] = false;
    }
}

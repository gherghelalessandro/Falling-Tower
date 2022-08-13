using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    GameObject platform;

    string[] divideS, divideR;
    int Score;
    bool pause = false;
    public bool power1 = false, power2 = false;
    bool[] activepowers = {false,false};

    GameObject playerobj;

    void Start()
    {
        divideS = ScoreText.text.Split(":");
        divideR = RemainText.text.Split(":");
        Score = int.Parse(divideS[1]);
        RemainText.text = divideR[0] + ":" + spawner.remains;
        StartCoroutine(settrue((x)=>power1=x,0, 15));
        StartCoroutine(settrue((y) => power2=y,0, 30));
    }
    void Update()
    {
        
        if (End.gameover==true)
        {
            spawner.gameObject.SetActive(false);
            StartCoroutine(stopthegame(1));
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
            }
            if(Input.GetKeyDown(KeyCode.X)&&power2==true)
            {
                activepowers[1]=true;
                power2 = false;
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
                    StartCoroutine(littlepiece(5, playerobj));
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
                
            }
            ScoreText.text = divideS[0] + ":" + Score;
            RemainText.text = divideR[0] + ":" + spawner.remains;
            spawner.touch = false;
        }
    }
    void stoptheobjects()
    {
        
        GameObject[] PiecesObj = GameObject.FindGameObjectsWithTag("Ground1");
        Movement player = playerobj.GetComponent<Movement>();
        Movement[] pieces = new Movement[PiecesObj.Length];
        for (int i = 0; i < PiecesObj.Length; i++)
        {
            pieces[i] = PiecesObj[i].GetComponent<Movement>();
            pieces[i].freeze(pause);
        }
        player.freeze(pause);
    }
    void upthepiece()
    {
        playerobj = GameObject.FindGameObjectWithTag("tetris");
        playerobj.transform.position = new Vector3(playerobj.transform.position.x,End.transform.position.y+2);
    }
    
    IEnumerator settrue(System.Action< bool> active,int index,float timer)
    {
        yield return new WaitForSecondsRealtime(timer);
        if (activepowers[index] ==false)
        {
            active(true);
        }
        StartCoroutine(settrue(active,index,timer));    
    }
   
    IEnumerator stopthegame(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Destroy(GameObject.FindGameObjectWithTag("tetris"));
    }
    IEnumerator littlepiece(int time,GameObject piece)
    {
        piece.transform.localScale = new Vector3(0.6f, 0.6f, 0);
        yield return new WaitForSecondsRealtime(time);
        activepowers[1] = false;
    }
}

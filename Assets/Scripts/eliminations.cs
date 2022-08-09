using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class eliminations : MonoBehaviour
{
    public TextMeshProUGUI text;
    int score;
    string[] test;

    private void Start()
    {
        test = text.text.Split(":");
        score = int.Parse(test[1]);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("tetris"))
        {
            Destroy(collision.gameObject);
            
            score--;
            string newsscore = "";
            newsscore+= test[0]+":"+score;
            text.text = newsscore;


        }
    }
}

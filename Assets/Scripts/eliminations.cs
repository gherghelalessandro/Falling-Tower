using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        if(collision.collider.CompareTag("tetris")|| collision.collider.CompareTag("Ground"))
        {
            Destroy(collision.gameObject);
            if(score > 0)
            {
                score--;
                string newsscore = "";
                newsscore += test[0] + ":" + score;
                text.text = newsscore;
            }
        }
            
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public bool gameover = false;
    public void raisethebar()
    {
        transform.position +=new Vector3(0,1.3f);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Ground"))
        {
            gameover = true;
        }
    }
}

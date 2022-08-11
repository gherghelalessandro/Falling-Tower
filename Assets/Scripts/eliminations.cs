using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eliminations : MonoBehaviour
{
    public bool destroy = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("tetris")|| collision.collider.CompareTag("Ground1"))
        {
            Destroy(collision.gameObject);
            destroy = true;
        }
    }
}

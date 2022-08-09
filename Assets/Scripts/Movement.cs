using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 input;
    float speed=10, stop=10,Rotation;
    bool active = true;
    void Start()
    {
        rigid=GetComponent<Rigidbody2D>();
        rigid.drag = stop;
    }
    private void Update()
    {
        if(active==true)
        {
            input.x = Input.GetAxis("Horizontal");
            Rotation = Input.GetAxis("rotation") * 60f;
            transform.Rotate(new Vector3(0, 0, Rotation) * Time.deltaTime);
        }
        
    }
    void FixedUpdate()
    {
        rigid.AddForce(input * speed * Time.deltaTime, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            active = false;
        }
    }
}

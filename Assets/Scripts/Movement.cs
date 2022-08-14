using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movement : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 input;
    float stop=10,Rotation,movement;
    public bool active = true;
    void Start()
    {
        rigid=GetComponent<Rigidbody2D>();
        rigid.drag = stop;
        active = true;
    }
    private void Update()
    {
        if(active==true)
        {
            movement = Input.GetAxis("Horizontal");
            Rotation = Input.GetAxis("rotation") * 60f;
            this.rigid.mass += 0.1f * Time.deltaTime;
        }
        
    }
    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position+new Vector2(movement*Time.fixedDeltaTime,-rigid.mass*Time.deltaTime));
        rigid.MoveRotation(rigid.rotation+Rotation*Time.fixedDeltaTime);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Contains("Ground")&&active==true)
        {
            StartCoroutine(timeactive(1));
            this.tag = "Ground1";
        }
        if (collision.gameObject.CompareTag("Outside") && active == true)
        {
            active = false;
        }
        
    }
    IEnumerator timeactive(float timer)
    {
        yield return new WaitForSeconds(timer);
        active = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movement : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 input;
    float speed=3, stop=10,Rotation;
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
            input.x = Input.GetAxis("Horizontal");
            Rotation = Input.GetAxis("rotation") * 60f;
        }
        
    }
    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position+new Vector2(input.x * speed*Time.fixedDeltaTime,-rigid.mass*Time.deltaTime));
        rigid.MoveRotation(rigid.rotation+Rotation*Time.fixedDeltaTime);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Contains("Ground")&&active==true)
        {
            active = false;
            this.tag = "Ground1";
            this.rigid.mass = 3;

        }
        if (collision.gameObject.CompareTag("Outside") && active == true)
        {
            active = false;
        }
        
    }
    public void freeze(bool pause)
    {
        StartCoroutine(pausetime(pause));
    }
    private IEnumerator pausetime(bool pause)
    {
        rigid.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitUntil(() => pause == true);
        rigid.constraints = RigidbodyConstraints2D.None;
    }
}

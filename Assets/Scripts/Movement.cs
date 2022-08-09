using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movement : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 input;
    float speed=10, stop=10,Rotation;
    public bool active = true;
    public bool elimination = false;
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
            transform.Rotate(new Vector3(0, 0, Rotation) * Time.deltaTime);
        }
        
    }
    void FixedUpdate()
    {
        rigid.AddForce(input * speed * Time.deltaTime, ForceMode2D.Impulse);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground")&&active==true)
        {
            active = false;
            this.tag = "Ground";
            StartCoroutine(StopTime(1));
            FindObjectOfType<Spawn>().spawnthepiece();
        }
        if(collision.gameObject.CompareTag("Outside")&&active==true)
        {
            active = false;
            FindObjectOfType<Spawn>().spawnthepiece();
        }
    }
    public IEnumerator StopTime(float time)
    {
        rigid.constraints = RigidbodyConstraints2D.FreezePositionX;
        yield return new WaitForSecondsRealtime(time);
        rigid.constraints = RigidbodyConstraints2D.None;
    }


}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    public GameObject[] Models;
    GameObject[] pieces;
    GameObject linereference,camera1;
    [SerializeField]
    GameObject platform;
    [SerializeField]
    Image image;
    public bool touch = false;
    int randomindex,i=1;
    public int remains;
    void Start()
    {
        randomindex = Random.Range(0, Models.Length);
        linereference = GameObject.FindGameObjectWithTag("Finish");
        camera1 = GameObject.FindGameObjectWithTag("MainCamera");
        remains = 2;
        pieces = new GameObject[remains+1];
        for (int i = 0; i <= remains; i++)
        {
            randomindex = Random.Range(0, Models.Length);
            pieces[i] = Models[randomindex];
        }
        if(remains>0)
        {
            image.sprite = pieces[remains - 1].GetComponent<SpriteRenderer>().sprite;
        }
        StartCoroutine(waitime());
    }
    public IEnumerator waitime()
    {
        yield return new WaitForSecondsRealtime(4f);
        StartCoroutine(spawnthetetrispieces());
    }
    public IEnumerator spawnthetetrispieces()
    {
        GameObject newpiece = Instantiate(pieces[remains], new Vector2(Random.Range(-6, 6),linereference.transform.position.y+4), Quaternion.identity);
        Movement model=newpiece.GetComponent<Movement>();
        yield return new WaitUntil(() => (model.active==false));
        RemainCheck();
        touch = true;
        StartCoroutine(spawnthetetrispieces());
    }
    void RemainCheck()
    {
        if (remains > 0)
        {
            remains--;
            if(remains-1>=0)
            {
                image.sprite = pieces[remains - 1].GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                image.sprite=null;
            }
            
        }
        else
        {
            int raise;
            i++;
            remains = 3 * i;
            raise = 2 - (i /10);
            pieces = new GameObject[remains + 1];
            for (int i = 0; i <= remains; i++)
            {
                randomindex = Random.Range(0, Models.Length);
                pieces[i] = Models[randomindex];
            }
            linereference.transform.position += new Vector3(0,raise);
            camera1.transform.position += new Vector3(0, 1.5f, 0);
            createplatforms();
            image.sprite = pieces[remains-1].GetComponent<SpriteRenderer>().sprite;
        }
    }
    void createplatforms()
    {
        int randomnr = Random.Range(3, 5), randomp;
        int[] randomposition = { -7,-6,-5,5,6,7 };
        GameObject[] pieces = new GameObject[randomnr];
        for (int i = 0; i < randomnr; i++)
        {
            randomp = Random.Range(0, randomposition.Length);
            pieces[i] = Instantiate(platform, new Vector3(randomposition[randomp], linereference.transform.position.y + Random.Range(-6f, -4f)), Quaternion.identity);
        }
    }
}

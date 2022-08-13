using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] Models;
    GameObject[] pieces;
    GameObject linereference,camera1;
    GameOver End;
    [SerializeField]
    GameObject platform;
    public bool touch = false;
    int randomindex,i=1;
    public int remains;
    void Start()
    {
        randomindex = Random.Range(0, Models.Length);
        StartCoroutine(waitime());
        linereference = GameObject.FindGameObjectWithTag("Finish");
        camera1 = GameObject.FindGameObjectWithTag("MainCamera");
        End = linereference.GetComponent<GameOver>();
        remains = 1;
        pieces = new GameObject[remains+1];
        for (int i = 0; i <= remains; i++)
        {
            randomindex = Random.Range(0, Models.Length);
            pieces[i] = Models[randomindex];
        }
    }
    public IEnumerator waitime()
    {
        yield return new WaitForSecondsRealtime(4f);
        StartCoroutine(spawnthetetrispieces());
    }
    public IEnumerator spawnthetetrispieces()
    {
       
        GameObject newpiece = Instantiate(pieces[remains], new Vector2(Random.Range(-5, 5),linereference.transform.position.y+2), Quaternion.identity);
        Movement model=newpiece.GetComponent<Movement>();
        yield return new WaitUntil(() => (model.active==false));
        if (remains > 0)
        {
            remains--;
        }
        else
        {
            i++;
            remains = 3 * i;
            pieces = new GameObject[remains+1];
            for (int i = 0; i <= remains; i++)
            {
                randomindex = Random.Range(0, Models.Length);
                pieces[i] = Models[randomindex];
            }
            End.raisethebar();
            camera1.transform.position += new Vector3(0, 1, 0);
            this.transform.position += new Vector3(0, 2, 0);
            createplatforms();
        }
        touch = true;
        StartCoroutine(spawnthetetrispieces());
    }
    void createplatforms()
    {
        int random = Random.Range(3, 5), randomp;
        int[] randomposition = { -5, -4, 4, 5 };
        GameObject[] pieces = new GameObject[random];
        for (int i = 0; i < random; i++)
        {
            randomp = Random.Range(0, 3);
            pieces[i] = Instantiate(platform, new Vector3(randomposition[randomp], End.transform.position.y + Random.Range(-3f, 0f)), Quaternion.identity);
        }
    }
}

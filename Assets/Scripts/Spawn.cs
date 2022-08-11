using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject spawn;
    public bool touch = false;
    void Start()
    {
        StartCoroutine(spawnthepieces(spawn));   
    }

    public IEnumerator spawnthepieces(GameObject move)
    {
        GameObject newpiece = Instantiate(move, new Vector2(Random.Range(-5, 5), transform.position.y), Quaternion.identity);
        Movement model=newpiece.GetComponent<Movement>();
        yield return new WaitUntil(() => (model.active==false));
        touch = true;
        StartCoroutine(spawnthepieces(move));
    }

}

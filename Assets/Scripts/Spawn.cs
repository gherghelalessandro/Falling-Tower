using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject spawn;
    Transform spawnTransform;
    void Start()
    {
        spawnTransform=GetComponent<Transform>();
        spawnthepiece();
    }
    public void spawnthepiece()
    {
        Instantiate(spawn,transform.position,Quaternion.identity);
    }

}

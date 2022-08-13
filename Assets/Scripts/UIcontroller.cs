using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    float move;
    [SerializeField]
    Button[] buton;
    public int index = 0;
    ColorBlock colorY, colorW;
    private void Start()
    {
        buton[index].image.color = Color.blue;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (index > 0)
            {
                buton[index].image.color =Color.white;
                index--;
                buton[index].image.color =Color.blue;

            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (index + 1 < buton.Length)
            {
                buton[index].image.color = Color.white;
                index++;
                buton[index].image.color = Color.blue;
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            buton[index].Select();
        }
    }
}

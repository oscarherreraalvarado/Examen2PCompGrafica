using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorVida : MonoBehaviour
{
  public int vida;
    private GameObject[] corazonesvida = new GameObject[3];
    void Start()
    {
        corazonesvida[0] = GameObject.Find("Corazon Vida 1");
        corazonesvida[1] = GameObject.Find("Corazon Vida 2");
        corazonesvida[2] = GameObject.Find("Corazon Vida 3");
        vida = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (vida == 3)
        {
            corazonesvida[0].SetActive(true);
            corazonesvida[1].SetActive(true);
            corazonesvida[2].SetActive(true);
        }
        else if (vida == 2)
        {
            corazonesvida[0].SetActive(true);
            corazonesvida[1].SetActive(true);
            corazonesvida[2].SetActive(false);
        }
        else if (vida == 1)
        {
            corazonesvida[0].SetActive(true);
            corazonesvida[1].SetActive(false);
            corazonesvida[2].SetActive(false);
        }
        else if (vida == 0)
        {
            corazonesvida[0].SetActive(false);
            corazonesvida[1].SetActive(false);
            corazonesvida[2].SetActive(false);
        }
    }
}


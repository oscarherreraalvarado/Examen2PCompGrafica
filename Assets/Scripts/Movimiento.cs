using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public GameObject[] waypoints;
    public float plataformSpeed = 2f;  // Agregué 'f' para definir que es un float
    private int waypointsIndex = 0;

    // El método debe ser void y no public.
    void Update()
    {
        MovePlatform();
    }

    // El método debe ser void y no public.
    void MovePlatform()
    {
        // Corrigí la sintaxis de Vector3.Distance.
        if (Vector3.Distance(transform.position, waypoints[waypointsIndex].transform.position) < 0.1f)
        {
            waypointsIndex++;
            // Cambié el símbolo ≥ por >.
            if (waypointsIndex >= waypoints.Length)
            {
                waypointsIndex = 0;
            }
        }

        // Corregí el uso de '=' a 'plataformSpeed * Time.deltaTime' en lugar de '='.
        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointsIndex].transform.position, plataformSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
            collision.gameObject.transform.SetParent(transform); // Cambié 'setParent' a 'SetParent'.
       // }
    }

    private void OnCollisionExit(Collision collision)
    {
       // if (collision.gameObject.CompareTag("Player"))
        //{
            collision.gameObject.transform.SetParent(null); // Cambié 'setParent' a 'SetParent'.
        //}
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo : MonoBehaviour
{
    public int vida = 3;

    GameObject jugador;
    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.Find("Atun _Character");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(jugador.transform);
        GetComponent<Rigidbody>().velocity = transform.forward;
    }
}

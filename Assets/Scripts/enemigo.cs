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
        GetComponent<Rigidbody>().velocity = transform.forward * 2;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Atun _Character") 

        Debug.Log("Miau, nos acaban de agarrar");

        if (collision.collider.tag == "Bala")
        {
            Destroy(collision.collider.gameObject);
            vida = vida - 1;
            if (vida == 0)
                Destroy(gameObject);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject prefabEnemigo;
    public int maximo_enemigos;
    public int numero_enemigos_actual = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GenerarEnemigo", 0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.R))
        //GenerarEnemigo();
    }
    void GenerarEnemigo()
    {
        if(numero_enemigos_actual < maximo_enemigos)
        {
            Instantiate(prefabEnemigo);
            numero_enemigos_actual = numero_enemigos_actual + 1;
        }
    }
}

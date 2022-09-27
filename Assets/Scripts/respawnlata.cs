using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnlata : MonoBehaviour
{
    public GameObject prefabrespawn;
    public int numero_de_latas;
    public int numero_max_latas;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GenerarLata", 1, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerarLata()
    {
        if (numero_de_latas < numero_max_latas)
        {
            Instantiate(prefabrespawn);
            numero_de_latas = numero_de_latas + 1;
        }
    }
}

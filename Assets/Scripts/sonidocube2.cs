using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sonidocube2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Cube B")
            Debug.Log("hemos entrado al cubo A");
        GetComponent<AudioSource>().Play();
    }
}

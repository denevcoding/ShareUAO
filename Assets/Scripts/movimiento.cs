using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimiento : MonoBehaviour
{
    public float velocidad;
    public int numerodeitems;
    public GameObject prefabBala;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * velocidad, 0, Input.GetAxis("Vertical") * velocidad);
        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
         
        if(Input.GetButtonDown("Fire1"))
        {
            GameObject balaAuxiliar = Instantiate( prefabBala, transform.position + transform.forward * 5, Quaternion.identity);
            balaAuxiliar.GetComponent<Rigidbody>().AddForce( transform.forward * 1000);
            Destroy(balaAuxiliar, 3);
           
        }

        
    }

   // private void OnTriggerEnter(Collider other)
   // {
       // if(other.name == "Cube A")
        //Debug.Log("hemos entrado al cubo A");
        //GetComponent<AudioSource>().Play();
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "item")
            Debug.Log("hemos cogido un item");
        Destroy(other.gameObject);
        numerodeitems = numerodeitems + 1;
    }
    private void OnTriggerStay(Collider other)
    {
       // if (other.tag == "zona")
           // Debug.Log("estamos en zona");
    }
}

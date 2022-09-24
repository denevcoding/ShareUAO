using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickerItems : MonoBehaviour
{
    public int numerodeitems;
 //   public PlayerUI uiPlayer;

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
        if (other.tag == "item")
        {
            Debug.Log("hemos cogido una lata de Atún");
            
            numerodeitems = numerodeitems + 1;

           // uiPlayer.SetAtunValue(numerodeitems); // Setting the ui text for atun amount

           // if (other.name == "pCylinder3")
             //   Debug.Log("hemos entrado al cubo A");

            //GetComponent<AudioSource>().Play();

            Destroy(other.gameObject);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "item"){
            Debug.Log("hemos cogido una lata de Atún");
            Destroy(collision.gameObject);
            numerodeitems = numerodeitems + 1;
        }
           
    }

    public void SubstractItem()
    {
        if (numerodeitems > 0) {

            numerodeitems = numerodeitems - 1;
           // uiPlayer.SetAtunValue(numerodeitems); // Setting the ui text for atun amount

        }        
                 
    }

}

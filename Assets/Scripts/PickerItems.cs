using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickerItems : MonoBehaviour
{
    public int numerodeitems;
    public int numerodeMedallones;
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
            Debug.Log("hemos cogido una lata de At�n");
            
            numerodeitems = numerodeitems + 1;

           // uiPlayer.SetAtunValue(numerodeitems); // Setting the ui text for atun amount

            if (other.tag == "medallon_Curry")
               Debug.Log("��Hemos recogido un Medall�n!!");

            numerodeMedallones = numerodeMedallones + 1;

            //GetComponent<AudioSource>().Play();

            Destroy(other.gameObject);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "item")
        {
            Debug.Log("hemos cogido una lata de At�n COLLISION");
            
            numerodeitems = numerodeitems + 1;

          


            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "medallon_Curry")
        {
            Debug.Log("hemos cogido una lata de At�n COLLISION");

            numerodeitems = numerodeitems + 1;




            Destroy(collision.gameObject);
        }



    }

    public void SubstractItem()
    {
        if (numerodeitems > 0)
        {

            numerodeitems = numerodeitems - 1;
            // uiPlayer.SetAtunValue(numerodeitems); // Setting the ui text for atun amount

        }

    }
}




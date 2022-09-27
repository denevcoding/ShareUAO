using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PickerItems : MonoBehaviour
{
    public int numerodeitems;
    public int numerodeMedallones;
    public int numerodeVidas;
    public PlayerUI uiPlayer;

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

           uiPlayer.SetAtunValue(numerodeitems); // Setting the ui text for atun amount

            if (other.tag == "medallon_Curry")
               Debug.Log("¡¡Hemos recogido un Medallón!!");

            numerodeMedallones = numerodeMedallones + 1;

            //GetComponent<AudioSource>().Play();

            Destroy(other.gameObject);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "item")
        {
            Debug.Log("hemos cogido una lata de Atún COLLISION");
            
            numerodeitems = numerodeitems + 1;

          


            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "medallon_Curry")
        {
            Debug.Log("hemos cogido una lata de Atún COLLISION");

            numerodeMedallones = numerodeMedallones + 1;




            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "enemigo")
        {
            Debug.Log("miau nos acaban de agarrar");

            numerodeVidas = numerodeVidas - 1;


            if (numerodeVidas == 0)

            {
                SceneManager.LoadScene("GameOVer");
            }


            // Destroy(collision.gameObject);
        }




    }

    public void SubstractItem()
    {
        if (numerodeitems > 0)
        {

            numerodeitems = numerodeitems - 1;
            uiPlayer.SetAtunValue(numerodeitems); // Setting the ui text for atun amount

        }

      
    }
}




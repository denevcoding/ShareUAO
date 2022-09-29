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
    public CatSoundManager soundManager;

    public AudioClip lataAtunClip;
    public AudioClip medallonClip;
    public AudioClip hurtClip;

    // Start is called before the first frame update
    void Start()
    {
        soundManager = GetComponentInChildren<CatSoundManager>();

        uiPlayer.SetAtunValue(numerodeitems);
        uiPlayer.SetLifeValue(numerodeVidas);
        uiPlayer.SetMedallionValue(numerodeMedallones);
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

           uiPlayer.SetAtunValue(numerodeitems); // Setting the ui text for atun amount

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
            uiPlayer.SetAtunValue(numerodeitems);
            soundManager.PlayOneShot(lataAtunClip, 1f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "medallon_Curry")
        {
            Debug.Log("hemos cogido una lata de At�n COLLISION");
            numerodeMedallones = numerodeMedallones + 1;
            uiPlayer.SetMedallionValue(numerodeMedallones);
            soundManager.PlayOneShot(medallonClip, 1f);
            Destroy(collision.gameObject);
        }


        if (collision.gameObject.tag == "enemigo")
        {
            Debug.Log("miau nos acaban de agarrar");
            numerodeVidas = numerodeVidas - 1;
            uiPlayer.SetLifeValue(numerodeVidas);

            soundManager.PlayOneShot(hurtClip, 1f);

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




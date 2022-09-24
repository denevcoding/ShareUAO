using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonPausa : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject menu;



    public void Resume ()
    {
        //gameObject.SetActive(false);

        Time.timeScale = 1f;

        GameIsPaused = false;

    }

    public void Pause ()

    {
        menu.SetActive(true);
        //gameObject.SetActive(true);

        Time.timeScale = 0f;

        GameIsPaused = true;

    }



}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonPausa : MonoBehaviour
{
public static bool GameIsPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void Resume ()

    {

        BotonPausa.SetActive(false);

        Time.timeScale = 1f;

        GameIsPaused = false;

    }

    void Pause ()

    {

        BotonPausa.SetActive(true);

        Time.timeScale = 0f;

        GameIsPaused = true;

    }



}
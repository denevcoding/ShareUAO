using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSoundManager : MonoBehaviour
{
    public AudioSource catSource;
    public AudioSource vfxSource;

    [Space(20)]
    public AudioClip step;
    public AudioClip stepChunco;
    // Start is called before the first frame update


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(AudioClip clip) 
    {
        if (catSource.clip != clip) 
        {
            catSource.clip = clip;
            catSource.Play();
        }
       
    }

    public void PlayOneShot(AudioClip clip, float volume)
    {
        catSource.PlayOneShot(clip, volume);
    }



    public void CleanClip()
    {
        if (catSource != null)
            catSource.clip = null;
    }




    public void PlayStep(int stepIndex)
    {
        if (stepIndex ==0)
        {
            catSource.PlayOneShot(step, 0.2f);
        }
        else if (stepIndex == 1)
        {
            catSource.PlayOneShot(stepChunco, 0.08f);
        }
    }

    public void PlayStepRunning(int stepIndex)
    {
        if (stepIndex == 0)
        {
            catSource.PlayOneShot(step, 0.5f);
        }
        else if (stepIndex == 1)
        {
            catSource.PlayOneShot(stepChunco, 0.5f);
        }
    }
}

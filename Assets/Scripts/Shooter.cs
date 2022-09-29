using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{    
    public int numerodeitems;
    public GameObject Bullet;
    public Transform soket;

    public float shootForce;

    public PickerItems picker;

    public CatSoundManager soundManager;
    public AudioClip ShootClip;

    // Start is called before the first frame update
    void Start()
    {
        picker = GetComponent<PickerItems>();
        soundManager = GetComponentInChildren<CatSoundManager>();
        // picker = GetComponent<PickerItems>();   
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (picker.numerodeitems > 0)
            {
                GameObject balaAuxiliar = Instantiate(Bullet, soket.position, Quaternion.identity);

                balaAuxiliar.GetComponent<item>().SwitchPhysics(false);
                balaAuxiliar.GetComponent<item>().DestroyAfterTime();

                balaAuxiliar.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce);

                soundManager.PlayOneShot(ShootClip, 1f);
                picker.SubstractItem();               
            }
        }
    }


}

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

    // Start is called before the first frame update
    void Start()
    {
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
                balaAuxiliar.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce);
                
                picker.SubstractItem();

                Destroy(balaAuxiliar, 2);
            }
        }
    }


}

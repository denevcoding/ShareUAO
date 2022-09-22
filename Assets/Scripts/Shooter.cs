using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{    

    public int numerodeitems;
    public GameObject Bullet;
    public Transform soket;

    public float shootForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            GameObject balaAuxiliar = Instantiate(Bullet, soket.position, Quaternion.identity);
            balaAuxiliar.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce);
            Destroy(balaAuxiliar, 2);

        }
    }


}

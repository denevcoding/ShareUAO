using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float force;
    public GameObject bullet = null;
    public Transform spawnerBullet;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }


    void Shoot() 
    {
        GameObject tempBullet = Instantiate(bullet);
        tempBullet.transform.position = spawnerBullet.position;
        tempBullet.GetComponent<Rigidbody>().AddForce(transform.right * force, ForceMode.Impulse);
    }
}

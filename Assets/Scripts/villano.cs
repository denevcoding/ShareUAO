using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class villano : MonoBehaviour
{
    public GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(item.transform);
        GetComponent<Rigidbody>().velocity = transform.right * 5;
    }
}

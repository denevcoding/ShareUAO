using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class villano : MonoBehaviour
{
    public GameObject item;
    public int velocidad;
    public int vida;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (item != null)
        {
            transform.LookAt(item.transform);
            GetComponent<Rigidbody>().velocity = transform.right * velocidad ;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Atun Character")
        {
            Debug.Log("Miau, nos acaban de agarrar");
            SceneManager.LoadScene(0);
            

        }


        if (collision.collider.tag == "Bala")
        {
            Destroy(collision.collider.gameObject);
            vida = vida - 1;
            if (vida == 0)
                Destroy(gameObject);
        }
    }

    }

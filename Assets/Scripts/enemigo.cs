using UnityEngine;
using UnityEngine.SceneManagement;

public class enemigo : MonoBehaviour
{
    public int vida = 3;
    public int comportamiento = 1; // 1 va a ser para perseguir y el 2 para patrullar
    public GameObject jugador;
    // Start is called before the first frame update
    void Start()
    {
       // jugador = GameObject.Find("Atun _Character");
        comportamiento = Random.Range(1, 3);

        InvokeRepeating("CambiarDireccion", 3, 3);
    }

    // Update is called once per frame
    void Update()
    {
       if(comportamiento == 1)

        {
            transform.LookAt(jugador.transform);
            GetComponent<Rigidbody>().velocity = transform.forward * 2;
        }
        else
        {
            GetComponent<Rigidbody>().velocity = transform.forward ;

        }
       
    }

    void CambiarDireccion()
    {
        transform.Rotate(0, Random.Range(0, 360), 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Atun _Character")
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

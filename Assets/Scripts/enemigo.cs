using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class enemigo : MonoBehaviour
{
    public int vida = 3;
    public int comportamiento = 1; // 1 va a ser para perseguir y el 2 para patrullar
    public GameObject jugador;


    public Camera cam;
    public NavMeshAgent agent;

    public LevelManager lvlManager;

    public Transform currentPoint;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        lvlManager = GameObject.FindObjectOfType<LevelManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        int indexPoint = (int)Random.Range(0, lvlManager.points.Count);
        currentPoint = lvlManager.points[indexPoint];

       // jugador = GameObject.Find("Atun _Character");
        //comportamiento = Random.Range(1, 3);

        //InvokeRepeating("CambiarDireccion", 3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPoint != null)
        {
            agent.SetDestination(currentPoint.position);
        }

        CheckPoint();
        //if(comportamiento == 1)

        // {
        //     transform.LookAt(jugador.transform);
        //     GetComponent<Rigidbody>().velocity = transform.forward * 2;
        // }
        // else
        // {
        //     GetComponent<Rigidbody>().velocity = transform.forward ;

        // }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Position " + hit.point);
                agent.SetDestination(hit.point);
            }
        }

    }

    public void CheckPoint()
    {
        float distance = Vector3.Distance(transform.position, currentPoint.position);
        if (distance <= 0.5f)
        {
            int indexPoint = (int)Random.Range(0, lvlManager.points.Count);
            currentPoint = lvlManager.points[indexPoint];
        }
    }

    void CambiarDireccion()
    {
        //transform.Rotate(0, Random.Range(0, 360), 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Atun Character")
        {
            Debug.Log("Miau, nos acaban de agarrar");
            //SceneManager.LoadScene(0);

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

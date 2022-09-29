using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;


public enum EnemyState
{
    Patrolling,
    Seeking
}

public class enemigo : MonoBehaviour
{
    public Camera cam;
    public EnemyState enemyState = EnemyState.Patrolling;
    public Animator enemyAnimator;

    [Header("Stats")]
    public int vida = 3;
    public int comportamiento = 1; // 1 va a ser para perseguir y el 2 para patrullar
    public float movMagnitude;

    [Header("Target")]
    public GameObject jugador;

    [Header("AI")]
    public NavMeshAgent agent;
    public LevelManager lvlManager;
    public Transform currentPoint;
    public float distanceToDetect;
    public float distanceToSeek;


    [Header("Sounds")]
    public CatSoundManager soundManager;
    public AudioClip hurtClip;



    private void Awake()
    {
        soundManager = GetComponentInChildren<CatSoundManager>();
        enemyAnimator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //lvlManager = GameObject.FindObjectOfType<LevelManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        int indexPoint = (int)Random.Range(0, lvlManager.points.Count);
        currentPoint = lvlManager.points[indexPoint];
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState == EnemyState.Patrolling)
        {
            Patrolling();
            enemyAnimator.SetBool("Walking", true);
            enemyAnimator.SetBool("Running", false);
        }
        else if (enemyState == EnemyState.Seeking)
        {
            Seeking();
            enemyAnimator.SetBool("Walking", false);
            enemyAnimator.SetBool("Running", true);
        }

        movMagnitude = agent.velocity.magnitude;
      
        if (agent.velocity.magnitude < 0.15f)
        {
            enemyAnimator.SetBool("Walking", false);
            enemyAnimator.SetBool("Running", false);
        }
    }

    public void Patrolling()
    {
        float distance = Vector3.Distance(transform.position, jugador.transform.position);

        if (distance < distanceToDetect)
        {
            agent.speed = 3;
            enemyState = EnemyState.Seeking;           
        }
        if (currentPoint != null)
        {
            agent.SetDestination(currentPoint.position);           
        }

        CheckPoint();
    }

    public void Seeking()
    {
        float distance = Vector3.Distance(transform.position, jugador.transform.position);
        if (distance > distanceToSeek)
        {
            agent.speed = 1.8f;
            enemyState = EnemyState.Patrolling;
        }
        else
        {
            Vector3 targetPos = ConvertPointToNavmesh(jugador.transform.position);
            agent.SetDestination(targetPos);
        }
        
    }


    public Vector3 ConvertPointToNavmesh(Vector3 position)
    {

        NavMeshHit hit;
        NavMesh.SamplePosition(position, out hit, 10f, 1);
        Vector3 finalPosition = hit.position;

        return finalPosition;
    }

    public void CheckPoint()
    {
        float distance = Vector3.Distance(transform.position, currentPoint.position);
        //Debug.Log("Distance To Point " + distance);
        if (distance <= 0.5f)
        {
            currentPoint = null;

            int indexPoint = (int)Random.Range(0, lvlManager.points.Count);
            if (!lvlManager.points[indexPoint] == currentPoint)
            {
                currentPoint = lvlManager.points[indexPoint];
            }
        }
    }

   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Atun Character")
        {
            Debug.Log("Miau, nos acaban de agarrar");
            //SceneManager.LoadScene(0);

        }        

        if (collision.collider.tag == "item")
        {
            soundManager.PlayOneShot(hurtClip, 1f);
            Destroy(collision.collider.gameObject);
            vida = vida - 1;
            if (vida == 0)
                Destroy(gameObject);
        }
    }
}

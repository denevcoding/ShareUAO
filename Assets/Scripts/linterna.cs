using UnityEngine;

public class linterna : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
    }
}

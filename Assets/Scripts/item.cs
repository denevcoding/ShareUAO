using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Atun,
    Medallon
}

public class item : MonoBehaviour
{
    public ItemType itemType;
    public bool isStatic;

    [Space(20)]
    public Collider itemCollider;
    public Rigidbody rigidBodie;

    [Space(20)]
    public float timeToDestroy;
    public float rotationSepeed = 5;

    private void Awake()
    {
        if (itemType == ItemType.Atun)
        {
            itemCollider = GetComponent<Collider>();
            rigidBodie = GetComponent<Rigidbody>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void SwitchPhysics(bool value)
    {
        isStatic = value;
        rigidBodie.isKinematic = isStatic;
        itemCollider.isTrigger = isStatic;      
    }

    public void DestroyAfterTime()
    {
        Destroy(this.gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSepeed, 0);   
    }
}

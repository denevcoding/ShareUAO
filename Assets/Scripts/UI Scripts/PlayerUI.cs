using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI atunText;

    // Start is called before the first frame update
    void Start()
    {
        SetAtunValue(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAtunValue(int value)
    {
        atunText.text = value.ToString();
    }
}

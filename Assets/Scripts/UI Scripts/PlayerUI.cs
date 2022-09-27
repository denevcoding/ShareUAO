using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI atunText;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI medallionText;

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

    public void SetLifeValue(int value)
    {
        lifeText.text = value.ToString();
    }

    public void SetMedallionValue(int value)
    {
        medallionText.text = value.ToString();
    }
}

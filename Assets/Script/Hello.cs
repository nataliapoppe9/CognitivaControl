using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hello : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void HelloWorld(GameObject text)
    {
        text.GetComponent<TextMeshProUGUI>().text = "HELLO WORLD";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

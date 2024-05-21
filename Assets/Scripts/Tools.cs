using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tools : MonoBehaviour
{
    public Text message;
    public static bool isFound;
    public GameObject printer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowFound()
    {
        //message.text = "found target!";
        isFound = true;
    }

    public void ShowLost()
    {
        //message.text = "lost target!";
        isFound = false;
    }
}

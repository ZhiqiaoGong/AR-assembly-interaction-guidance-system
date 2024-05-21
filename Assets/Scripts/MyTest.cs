using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTest : MonoBehaviour
{
    public GameObject hint1, hint2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Atan2(Vector3.Dot(hint1.transform.up, Vector3.Cross(hint1.transform.forward, hint2.transform.position - hint1.transform.position)), Vector3.Dot(hint1.transform.forward, hint2.transform.position - hint1.transform.position)) * Mathf.Rad2Deg;
        Debug.Log(angle);
    }
}

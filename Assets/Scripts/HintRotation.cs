using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintRotation : MonoBehaviour
{
    public Camera arcamera;
    public GameObject hint1, hint2, hint3, hint4, hint5, hint6, printer, plane;
    public Plane p;

    Vector3 vectorUp, vector2, vector3, vector4, vector5;
    public Text message;
    bool isFirstDetect;
    Vector3 preCameraVec, cameraVec, preAngle, prePos;

    // Start is called before the first frame update
    void Start()
    {
        isFirstDetect = true;

        vectorUp = new Vector3(0, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Tools.isFound)
        {

            vectorUp = printer.transform.TransformDirection(new Vector3(0, printer.transform.localEulerAngles.y,0));

            Debug.DrawRay(printer.transform.position, vectorUp, Color.red);

            p = new Plane(vectorUp.normalized, printer.transform.position);       

            hint1.transform.LookAt(p.ClosestPointOnPlane(arcamera.transform.position),vectorUp);

            hint2.transform.LookAt(p.ClosestPointOnPlane(arcamera.transform.position), vectorUp);

            hint3.transform.LookAt(p.ClosestPointOnPlane(arcamera.transform.position), vectorUp);

            hint4.transform.LookAt(p.ClosestPointOnPlane(arcamera.transform.position), vectorUp);

            hint5.transform.LookAt(p.ClosestPointOnPlane(arcamera.transform.position), vectorUp);

            hint6.transform.LookAt(p.ClosestPointOnPlane(arcamera.transform.position), vectorUp);
        }

        
    }
}

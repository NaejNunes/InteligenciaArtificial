using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{
    public float speed = 10;
    public float rotationSpeed = 100;
    public bool autoPilot = false;
    public GameObject fuel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        transform.Translate(0, translation, 0);

        transform.Rotate(0, 0, -rotation);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CalculateDistance();
            CalculateAngle();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            autoPilot = !autoPilot;
        }
        if (autoPilot)
        {
            if(CalculateDistance() > 1)
                AutoPilot();

        }
    }

     float CalculateDistance()
    {
        Vector3 tp = this.transform.position;
        Vector3 fp = fuel.transform.position;

        float distance = Mathf.Sqrt(
                            Mathf.Pow(tp.x - fp.x, 2) + Mathf.Pow(tp.y - fp.y, 2));

        float unityDistance = Vector3.Distance(tp, fp);
                                
        Debug.Log("Distance: " + distance);
        Debug.Log("Unity Distance: " + unityDistance);

        return distance;
    }

    float AutoSpeed = 0.1f;

    void AutoPilot()
    {
        CalculateAngle();
        this.transform.Translate(this.transform.up * AutoSpeed, Space.World);
    }
    void CalculateAngle()
    {
        Vector3 tF = this.transform.up;
        Vector3 fD = fuel.transform.position - this.transform.position;

        float dot = tF.x * fD.x + tF.y * fD.y;
        float angle = Mathf.Acos(dot / (tF.magnitude * fD.magnitude));

        Debug.Log("Angle: " + angle * Mathf.Rad2Deg);
        Debug.Log("Unity Angle: " + Vector3.Angle(tF, fD));

        Debug.DrawRay(this.transform.position, tF * 10, Color.green, 2);
        Debug.DrawRay(this.transform.position, fD, Color.red, 2);

        int clockwise = 1;

        if (Cross(tF, fD).z < 0)
            clockwise = -1;

        float unityAngle = Vector3.SignedAngle(tF, fD, this.transform.forward);

        this.transform.Rotate(0, 0, unityAngle * 0.08f);
    }

    Vector3 Cross(Vector3 v, Vector3 w)
    {
        float xMult = v.y * w.z - v.z * w.y;
        float yMult = v.z * w.x - v.x * w.z;
        float ZMult = v.x * w.y - v.y * w.x;

        Vector3 crossProd = new Vector3(xMult, yMult, ZMult);
        return crossProd;
    }

}

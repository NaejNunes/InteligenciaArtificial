using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{
    public float speed = 10;
    public float rotationSpeed = 100;

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
    }

     void CalculateDistance()
    {
        Vector3 tp = this.transform.position;
        Vector3 fp = fuel.transform.position;

        float distance = Mathf.Sqrt(
                            Mathf.Pow(tp.x - fp.x, 2) + Mathf.Pow(tp.y - fp.y, 2));

        float unityDistance = Vector3.Distance(tp, fp);
                                
        Debug.Log("Distance: " + distance);
        Debug.Log("Unity Distance: " + unityDistance);
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
    }
}

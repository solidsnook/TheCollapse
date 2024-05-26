using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    public GameObject RotatePoint;
    public float AngleSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(RotatePoint.transform.position, new Vector3(0,1,0), AngleSpeed * Time.deltaTime);
    }
}

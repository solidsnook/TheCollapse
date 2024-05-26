using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject PlanetCenter;
    public GameObject PlanetController;
    public int RotateSpeed;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    //Coments
    // Update is called once per frame
    void Update()
    {
       if(Input.GetKey(KeyCode.A)) 
        {
            PlanetCenter.transform.RotateAround(PlanetController.transform.localPosition, PlanetController.transform.up, RotateSpeed * Time.deltaTime);
           // PlanetCenter.transform.Rotate(0, RotateSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            PlanetCenter.transform.RotateAround(PlanetController.transform.localPosition, PlanetController.transform.up, -RotateSpeed * Time.deltaTime);
          //  PlanetCenter.transform.Rotate(0, -RotateSpeed * Time.deltaTime, 0, Space.World);
        }
        if (Input.GetKey(KeyCode.W))
        {
            PlanetCenter.transform.RotateAround(PlanetController.transform.localPosition, PlanetController.transform.right, RotateSpeed * Time.deltaTime);
           // PlanetCenter.transform.Rotate(RotateSpeed * Time.deltaTime, 0, 0, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            PlanetCenter.transform.RotateAround(PlanetController.transform.localPosition, PlanetController.transform.right, -RotateSpeed * Time.deltaTime);
          //  PlanetCenter.transform.Rotate(-RotateSpeed * Time.deltaTime, 0, 0,Space.World);
        }
        if (Input.GetKey(KeyCode.Q)) 
        {
            PlanetCenter.transform.RotateAround(PlanetController.transform.localPosition, PlanetController.transform.forward, RotateSpeed * Time.deltaTime);
           // PlanetCenter.transform.Rotate(0,0,-RotateSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.E))
        {
            PlanetCenter.transform.RotateAround(PlanetController.transform.localPosition, PlanetController.transform.forward, -RotateSpeed * Time.deltaTime);
            //PlanetCenter.transform.Rotate(0, 0, RotateSpeed * Time.deltaTime, Space.World);
        }
    }
}

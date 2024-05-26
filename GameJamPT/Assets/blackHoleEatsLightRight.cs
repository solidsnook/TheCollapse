using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//we should be brighter from the side facing away from the BH right? dunno

public class blackHoleEatsLightRight : MonoBehaviour
{
    //we're inside the directional light soure control room right now

    //we need one light in the bh and one facing it in a line drawn trough the planet from the bh
    public GameObject bhGlow;
    public GameObject bhGlowSource;
    public GameObject zaWarudo;

    // Start is called before the first frame update
    void Start()
    {

        
       
    }

    // Update is called once per frame
    void Update()
    {
        bhGlow.transform.position = bhGlowSource.transform.position;

        var lightposition = Vector3.zero;
        lightposition = Vector3.Normalize(zaWarudo.transform.position - bhGlowSource.transform.position);
        lightposition = lightposition * 100;
        transform.position = lightposition;
        transform.LookAt(bhGlowSource.transform.position);



    }
}

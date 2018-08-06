using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnRegion : MonoBehaviour {
    public GameObject section1, section2, section3;
    int GenSec;
    private GameObject NewSec;
    float NewZAxis;

    private void Start()
    {
        NewZAxis = AxisKeeper.theZAxis;


    }
    private void OnTriggerEnter(Collider other)
    {
        Vector3 NextPosition = new Vector3(50, 0, AxisKeeper.theZAxis);
        GenSec = Random.Range(1, 4);
        NewZAxis = AxisKeeper.theZAxis;

        if (GenSec == 1)
        {
            NewSec = section1;
        }
        if (GenSec == 2)
        {
            NewSec = section2;
        }
        if (GenSec == 3)
        {
            NewSec = section3;
        }
        Instantiate(NewSec, NextPosition,NewSec.transform.rotation );
        AxisKeeper.theZAxis += 240;
    }
}

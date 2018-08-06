using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisKeeper : MonoBehaviour {
    public static float theZAxis = 240.0f;
    private float InternalAxis;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InternalAxis = theZAxis;
    }
}

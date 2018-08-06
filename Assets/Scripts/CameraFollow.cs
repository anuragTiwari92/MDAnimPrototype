using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform lookat; //camera will look at player
    [SerializeField] Vector3 buffer = new Vector3(0, 3.0f, -2.65f);
    private void Start()
    {
        transform.position = lookat.position + buffer;
    }
    private void LateUpdate()
    {
        Vector3 CameraPostion = lookat.position + buffer;
        CameraPostion.x = 0;//dont want the camera bouncing left to right
        transform.position = Vector3.Lerp(transform.position,CameraPostion,Time.deltaTime);
    }

}

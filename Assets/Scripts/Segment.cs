using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour {

    public int Segment_Id { set; get; }

    public bool transition;

    public int length;
    public int beginy1, beginy2, beginy3;

    public int endy1, endy2, endy3;

    private Items[] items;

    private void Awake()
    {
        items = gameObject.GetComponentsInChildren<Items>();
    }
    public void Spawn()
    {
        gameObject.SetActive(true);
    }
    public void Despawn()
    {
        gameObject.SetActive(false);
    }
}

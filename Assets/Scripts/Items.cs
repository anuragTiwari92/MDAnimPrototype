using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    none = -1,
    jump = 0,
    debris = 1,
}
public class Items : MonoBehaviour {

    public ItemType type;
    public int VisualIndex;
	
}

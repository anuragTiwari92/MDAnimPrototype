using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {

    public ItemType type;
    private Items currentItem;

    public void Spawn()
    {
        //currentItem = GameManager.Instance.GetPiece(type, 0);
        currentItem.gameObject.SetActive(true);
        currentItem.transform.SetParent(transform, false);
    }

    public void DeSpawn()
    {
        currentItem.gameObject.SetActive(false);
    }
}

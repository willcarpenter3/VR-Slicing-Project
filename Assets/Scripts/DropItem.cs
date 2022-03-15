using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{

    public GameObject item;
    public void Drop()
    {
        Debug.Log("Dropping Item");
        Instantiate(item, transform.position, transform.rotation);
    }
}

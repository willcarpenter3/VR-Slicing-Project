using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpawn : MonoBehaviour
{

    private GameObject pool;

    public GameObject spawnable;

    public float poolYLocation;

    public float scaleIncrease;

    public void spawn(Transform transform)
    {
        Debug.Log("Calling pool spawn");
        if (pool == null)
        {
            Debug.Log("Going to Instantiate Pool");
            pool = Instantiate(spawnable, new Vector3(transform.position.x, poolYLocation, transform.position.z), Quaternion.identity);
        }
        else
        {
            Vector3 currentScale = pool.transform.localScale;
            pool.transform.localScale = new Vector3(currentScale.x + scaleIncrease, currentScale.y + scaleIncrease, currentScale.z + scaleIncrease);
        }
    }

    //Getters and setters for pool

    //Scale function
}

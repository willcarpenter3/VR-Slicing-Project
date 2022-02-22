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
        if (pool == null)
        {
            pool = Instantiate(spawnable, new Vector3(transform.position.x, poolYLocation, transform.position.z), transform.rotation);
        }
        else
        {
            Vector3 currentScale = pool.transform.localScale;
            pool.transform.localScale = new Vector3(currentScale.x + scaleIncrease, currentScale.y + scaleIncrease, currentScale.z + scaleIncrease);
        }
    }
}

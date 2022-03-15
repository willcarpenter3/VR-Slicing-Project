using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpawn : MonoBehaviour
{

    private GameObject pool;

    public GameObject spawnable;

    public float poolYLocation = 0.01f;

    public float scaleIncrease = 5;

    public float timeToAbsorb = 3;

    private bool isAbsorbing = false;

    public GameObject spawn(Transform transform)
    {
        Debug.Log("Calling pool spawn");
        if (pool == null)
        {
            Debug.Log("Going to Instantiate Pool");
            pool = Instantiate(spawnable, new Vector3(transform.position.x, poolYLocation, transform.position.z), Quaternion.identity);
        }
        return pool;
    }

    //Getters and setters for pool
    public GameObject getPool()
    {
        return pool;
    }

    public void setPool(GameObject p)
    {
        pool = p;
    }

    //Scale function
    public void scale()
    {
        Vector3 currentScale = pool.transform.localScale;
        pool.transform.localScale = new Vector3(currentScale.x * scaleIncrease, currentScale.y * scaleIncrease, currentScale.z * scaleIncrease);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            isAbsorbing = true;
            float x = transform.localScale.x / timeToAbsorb;
            float y = transform.localScale.y / timeToAbsorb;
            float z = transform.localScale.z / timeToAbsorb;
            StartCoroutine(Absorb(x, y, z));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isAbsorbing = false;
    }

    IEnumerator Absorb(float x, float y, float z)
    {
        while (isAbsorbing)
        {
            Vector3 currScale = transform.localScale;
            Vector3 newScale = new Vector3(currScale.x - x, currScale.y - y, currScale.z - z);
            transform.localScale = newScale;
            yield return new WaitForSeconds(1); 
        }
    }
}

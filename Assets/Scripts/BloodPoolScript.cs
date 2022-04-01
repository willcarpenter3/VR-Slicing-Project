using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPoolScript : MonoBehaviour
{
    public float timeToAbsorb = 3;

    private bool isAbsorbing = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter");
        if (other.CompareTag("Sword") && !isAbsorbing)
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
        if (other.CompareTag("Sword") && isAbsorbing)
        {
            Debug.Log("Trigger Exit");
            isAbsorbing = false;
        }
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

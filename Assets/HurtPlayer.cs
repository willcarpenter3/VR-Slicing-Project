using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    void Update()
    {
        GetComponent<Rigidbody>().MovePosition(new Vector3(transform.position.x + .01f, transform.position.y, transform.position.z));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().damagePlayer(25);
        }
    }
}

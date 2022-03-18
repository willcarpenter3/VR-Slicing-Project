using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Tooltip("Speed at which the projectile should move. This will scale with fixed delta time.")]
    [SerializeField] float projectileSpeed = 10f;

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * projectileSpeed * Time.fixedDeltaTime, Space.Self);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSlash : MonoBehaviour
{
    [Tooltip("Prefab for the projectile to be launched")]
    [SerializeField] private GameObject projectile;  

    [Tooltip("Point in space from which the projectile will launch")]
    [SerializeField] private Transform launcher;

    [Tooltip("Amount of time that a projectile should be active")]
    [SerializeField] private float projectileLifetime = 10f;

    /** Boolean to restrict firing of */
    private bool canShoot = true;

    public void FireProjectile()
    {
        if (canShoot)
        {
            GameObject p = Instantiate(projectile, launcher.position, Quaternion.LookRotation(transform.up, transform.right * -1));
            Destroy(p, projectileLifetime);
        }
    }
}

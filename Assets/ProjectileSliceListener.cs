using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSliceListener : MonoBehaviour
{
    [Tooltip("Reference to the associated Slicer component of the projectile")]
    [SerializeField] private Slicer slicer;

    [Tooltip("Cooldown timer used to prevent duplicate slices of the same object")]
    [SerializeField] private float sliceCooldown = 0.1f;

    /** Bool used to prevent duplicate slices */
    private bool canCut = true;

    private void OnTriggerEnter(Collider other)
    {
        if (canCut)
        {
            canCut = false;
            StartCoroutine("StartCooldown");
            slicer.isTouched = true;
        }
        
    }

    IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(sliceCooldown);
        canCut = true;
    }
}
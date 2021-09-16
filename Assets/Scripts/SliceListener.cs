using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Source: https://github.com/LandVr/SliceMeshes
 */
public class SliceListener : MonoBehaviour
{
    public Slicer slicer;
    private void OnTriggerEnter(Collider other)
    {
        slicer.isTouched = true;
    }
}

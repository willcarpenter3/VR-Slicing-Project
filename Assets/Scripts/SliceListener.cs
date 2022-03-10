using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

/**
 * Source: https://github.com/LandVr/SliceMeshes
 */
public class SliceListener : MonoBehaviour
{
    public Slicer slicer;

    public InputActionProperty velocityProperty;

    public XRBaseController rightHand;

    public float vibrationIntensity = 0.1f;

    public float minToCut = 4.0f;

    public Vector3 velocity { get; private set; } = Vector3.zero;

    public float sliceCooldown = 0.25f;

    [SerializeField] bool canCut = true;

    void Update()
    {
        velocity = velocityProperty.action.ReadValue<Vector3>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(canCut)
        {
            canCut = false;
            //Debug.Log("Velocity Magnitude: " + velocity.magnitude);
            //Debug.Log("Velocity x: " + velocity.x + " y: " + velocity.y + " z: " + velocity.z );
            if (velocity.magnitude >= minToCut)
            {
                slicer.isTouched = true;
                float finalIntensity = vibrationIntensity * (velocity.magnitude / 2);
                rightHand.SendHapticImpulse(finalIntensity, .3f);
            }
            StartCoroutine("StartCooldown");
        }
    }

    IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(sliceCooldown);
        canCut = true;
    }
}

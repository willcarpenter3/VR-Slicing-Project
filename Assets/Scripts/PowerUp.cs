using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowerUp : MonoBehaviour
{

    public enum EPowerUpType {GiantSword, ProjectileSlash, TimeSlow}

    [Header("General power-up information")]
    [SerializeField] private float powerUpTime = 20f;

    [SerializeField] private EPowerUpType selectedType;

    [Header("Input Information")]
    [SerializeField] private InputActionReference swapPowerUpReference;
    [SerializeField] private InputActionReference applyPowerUpReference;

    [Header("Meters for powerup charging")]
    [SerializeField] private float giantSwordMeter;
    [SerializeField] private float projectileSlashMeter;
    [SerializeField] private float timeSlowMeter;

    [Header("Giant Slash Information")]
    [SerializeField] private Transform swordTransform;
    [SerializeField] private Transform slicerTransform;

    void Awake()
    {
        //Subscribe to Input Events
        swapPowerUpReference.action.started += SwapPowerUp;
        applyPowerUpReference.action.started += ApplyPowerUp;
    }

    void OnDestroy()
    {
        //Unsubscribe from Input Events
        swapPowerUpReference.action.started -= SwapPowerUp;
        applyPowerUpReference.action.started -= ApplyPowerUp;
    }

    void SwapPowerUp(InputAction.CallbackContext context)
    {
        switch(selectedType)
        {
            case EPowerUpType.GiantSword:
                selectedType = EPowerUpType.ProjectileSlash;
                break;
            case EPowerUpType.ProjectileSlash:
                selectedType = EPowerUpType.TimeSlow;
                break;
            case EPowerUpType.TimeSlow:
                selectedType = EPowerUpType.GiantSword;
                break;
        }

        Debug.LogFormat("Selected Type: {0}", selectedType.ToString());
    }

    void ApplyPowerUp(InputAction.CallbackContext context)
    {
        Debug.Log("Applying Power Up");
        switch (selectedType)
        {
            case EPowerUpType.GiantSword:
                StartCoroutine("GiantSlash");
                break;
            case EPowerUpType.ProjectileSlash:
                //projectileSlashMeter += val;
                break;
            case EPowerUpType.TimeSlow:
                //timeSlowMeter += val;
                break;
        }
    }


    void Start()
    {
        //TODO remove - this is only for testing purposes
        //ApplyPowerUp(EPowerUpType.GiantSword);
    }


    void AddToMeter(EPowerUpType type, float val)
    {
        switch(type)
        {
            case EPowerUpType.GiantSword:
                giantSwordMeter += val;
                break;
            case EPowerUpType.ProjectileSlash:
                projectileSlashMeter += val;
                break;
            case EPowerUpType.TimeSlow:
                timeSlowMeter += val;
                break;
        }
    }

    private IEnumerator GiantSlash()
    {
        //Catch initial values so they can be reset
        Vector3 swordPos = swordTransform.localPosition;
        Vector3 swordScale = swordTransform.localScale;
        Vector3 slicerPos = slicerTransform.localPosition;
        Vector3 slicerScale = slicerTransform.localScale;

        //Set new values
        swordTransform.localScale = new Vector3(swordScale.x, swordScale.y * 2, swordScale.z);
        slicerTransform.localScale = new Vector3(slicerScale.x, slicerScale.y, slicerScale.z * 2);

        swordTransform.localPosition = new Vector3(swordPos.x, swordPos.y * 2, swordPos.z);
        slicerTransform.localPosition = new Vector3(slicerPos.x, slicerPos.y * 2, slicerPos.z);


        //Powerup duration
        yield return new WaitForSeconds(powerUpTime);

        //Reset values
        swordTransform.localScale = swordScale;
        slicerTransform.localScale = slicerScale;
        swordTransform.localPosition = swordPos;
        slicerTransform.localPosition = slicerPos;


    }
}

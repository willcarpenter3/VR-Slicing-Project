using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PowerUp : MonoBehaviour
{

    public enum EPowerUpType {GiantSword, TimeSlow, ProjectileSlash}

    [Header("General power-up information")]

    [Tooltip("Time that this power-up should last")]
    [SerializeField] private float powerUpTime = 20f;

    [SerializeField] private EPowerUpType selectedType = EPowerUpType.GiantSword;

    [SerializeField] private float meterFullValue = 10f;

    [Header("Input Information")]
    [SerializeField] private InputActionReference swapPowerUpReference;
    [SerializeField] private InputActionReference applyPowerUpReference;
    

    [Header("Giant Slash Information")]
    [SerializeField] private Transform swordTransform;
    [SerializeField] private Transform slicerTransform;

    [Header("Time Slow Information")]
    [SerializeField] private float timeSlowFactor = 0.5f;
    [SerializeField] private SnapTurnProviderBase snapTurnProvider;

    [Header("Projectile Slash Information")]
    [SerializeField] private ProjectileSlash projectileSlash;

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
        if(GameManager.Instance.powerUpMeters[selectedType] >= meterFullValue)
        {
            Debug.Log("Applying Power Up");
            switch (selectedType)
            {
                case EPowerUpType.GiantSword:
                    StartCoroutine("GiantSlash");
                    break;
                case EPowerUpType.ProjectileSlash:
                    projectileSlash.FireProjectile();
                    break;
                case EPowerUpType.TimeSlow:
                    StartCoroutine("TimeSlow");
                    break;
            }

            GameManager.Instance.powerUpMeters[selectedType] = 0f;
        }
        else 
        {
            Debug.Log("Meter not full!");
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
        yield return new WaitForSecondsRealtime(powerUpTime);

        //Reset values
        swordTransform.localScale = swordScale;
        slicerTransform.localScale = slicerScale;
        swordTransform.localPosition = swordPos;
        slicerTransform.localPosition = slicerPos;
    }

    private IEnumerator TimeSlow()
    {
        //Get copy of original values
        float originalFixedTimeScale = Time.fixedDeltaTime;
        float originalTimeScale = Time.timeScale;
        float originalSnapTurnScale = snapTurnProvider.debounceTime;

        //Slow Time
        Time.timeScale *= timeSlowFactor;
        Time.fixedDeltaTime *= timeSlowFactor;
        snapTurnProvider.debounceTime *= timeSlowFactor;
        
        
        //Powerup duration
        yield return new WaitForSecondsRealtime(powerUpTime);

        //Reset value
        Time.timeScale = originalTimeScale;
        Time.fixedDeltaTime = originalFixedTimeScale;
        snapTurnProvider.debounceTime = originalSnapTurnScale;
    }
}

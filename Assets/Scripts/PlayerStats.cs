using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerStats : MonoBehaviour
{

    [Header("Stats")]
    public float maxHealth = 100;
    public float health;
    public float maxPower = 100;
    private float power;

    private float maxWeight = 0.666f;

    [Header("Volume Reference")]
    public Volume volume;
    private Vignette vignette;

    private void Awake()
    {
        if (volume.profile.TryGet(out Vignette vignette))
            this.vignette = vignette;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        power = maxPower;
    }

    public void damagePlayer(int amt)
    {
        health = Mathf.Max(0, health - amt);
        Debug.Log("Health %: " + ((maxHealth - health) / maxHealth));
        volume.weight = maxWeight * ((maxHealth - health) / maxHealth);
    }
}

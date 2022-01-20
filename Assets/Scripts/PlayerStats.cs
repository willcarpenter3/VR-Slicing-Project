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

    [Header("Volume Reference")]
    public Volume volume;
    private Vignette vignette;

    private void Awake() {
        if (volume.profile.TryGet(out Vignette vignette))
            this.vignette = vignette;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        power = maxPower;
    }

    // Update is called once per frame
    void Update()
    {
        vignette.intensity.Override((maxHealth - health) / maxHealth);

        health -= 0.01f;
    }
}

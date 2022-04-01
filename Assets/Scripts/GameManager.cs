using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Player Information")]

    [SerializeField] private float maxHealth;

    /** Property for getting and setting the player's current health amount */
    public float CurrentHealth {get; set;}

    /**Dictionary to access power up meters by type */
    [SerializeField] public Dictionary<PowerUp.EPowerUpType, float> powerUpMeters;

    [Header("Game Statistics")]
    [Tooltip("Number of enemies killed by the player")]
    [SerializeField] private float killCount;
    [Tooltip("Number of times the player has made slices")]
    [SerializeField] private float totalSlices;
    [Tooltip("Current player kill combo")]
    [SerializeField] private int killCombo;
    [Tooltip("The maximum amount of time between kills to keep building a combo")]
    [SerializeField] private float maxComboTime = 5f;
    /** Bool to tell if a combo is currently running */
    private bool _inCombo;
    /** Internal timer for combo */
    private float _comboTimer;

    [Header("Player UI")]
    [Tooltip("Reference to the post processing volume for showing health")]
    [SerializeField] private Volume volume;
    /** Maximum allowed weight for the post-process volume */
    private float _maxWeight = 0.6f;

    [SerializeField] private TextMeshProUGUI giantSwordMeterText;
    [SerializeField] private TextMeshProUGUI timeSlowMeterText;
    [SerializeField] private TextMeshProUGUI projectileMeterText;

    [Header("Game UI")]
    [Tooltip("Reference to the UI element displaying the number of kills")]
    [SerializeField] private TextMeshProUGUI killAmtText;
    [Tooltip("Reference to the UI element displaying the number of slices")]
    [SerializeField] private TextMeshProUGUI sliceAmtText;
    [Tooltip("Reference to the UI element displaying the current kill combo")]
    [SerializeField] private TextMeshProUGUI comboAmtText;


    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        //Initialize fields
        powerUpMeters = new Dictionary<PowerUp.EPowerUpType, float>();
        powerUpMeters.Add(PowerUp.EPowerUpType.GiantSword, 100f);
        powerUpMeters.Add(PowerUp.EPowerUpType.TimeSlow, 100f);
        powerUpMeters.Add(PowerUp.EPowerUpType.ProjectileSlash, 100f);

        CurrentHealth = maxHealth;
    }

    void Update()
    {
        

        //Update combo timer
        if(_inCombo)
        {
            _comboTimer -= Time.deltaTime;
            Debug.LogFormat("Combo Timer: {0}", _comboTimer);
            if(_comboTimer <= 0)
            {
                _inCombo = false;
                killCombo = 0;
            }
        }

        //Update player UI
        giantSwordMeterText.text = powerUpMeters[PowerUp.EPowerUpType.GiantSword].ToString();
        timeSlowMeterText.text = powerUpMeters[PowerUp.EPowerUpType.TimeSlow].ToString();
        projectileMeterText.text = powerUpMeters[PowerUp.EPowerUpType.ProjectileSlash].ToString();
        
        //Update game UI
        comboAmtText.text = killCombo.ToString();
    }

    public void damagePlayer(int amt)
    {
        CurrentHealth = Mathf.Max(0, CurrentHealth - amt);
        Debug.Log("Health %: " + ((maxHealth - CurrentHealth) / maxHealth));
        volume.weight = _maxWeight * ((maxHealth - CurrentHealth) / maxHealth);
    }

    public void addSlice()
    {
        totalSlices++;
        sliceAmtText.text = totalSlices.ToString();
    }

    public void addKill()
    {
        killCount++;
        killAmtText.text = killCount.ToString();

        if (!_inCombo)
            _inCombo = true;
        _comboTimer = maxComboTime;
        killCombo++;
    }
}

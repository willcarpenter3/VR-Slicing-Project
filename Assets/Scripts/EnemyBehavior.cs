using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)]
    float enemyHeuristic;

    // Start is called before the first frame update
    void Start()
    {
        enemyHeuristic = Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getHeuristic() { return enemyHeuristic; }
    public void setHeuristic(float val) { enemyHeuristic = Mathf.Clamp(val, 0, 1); }
}

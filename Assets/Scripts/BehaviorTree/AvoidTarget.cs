using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class AvoidTarget : ActionNode
{
    public Transform target;
    private float heuristic;
    public float speed = 5;
    public float stoppingDistance = 10f;
    public bool updateRotation = true;
    public float acceleration = 40.0f;
    public float tolerance = 1.0f;
    public float minDistance = 10;
    protected override void OnStart()
    {
        heuristic = context.gameObject.GetComponent<EnemyBehavior>().getHeuristic();
        target = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        context.agent.SetDestination(target.position);
        

        if (context.agent.remainingDistance > minDistance)
        {
            // Reset
            return State.Success;
        }

        return State.Running;
    }
}

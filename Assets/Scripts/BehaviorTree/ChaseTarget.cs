using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class ChaseTarget : ActionNode
{
    public Transform target;
    public float speed = 5;
    public float stoppingDistance = 10f;
    public bool updateRotation = true;
    public float acceleration = 40.0f;
    public float tolerance = 1.0f;

    protected override void OnStart()
    {
        context.agent.stoppingDistance = stoppingDistance;
        context.agent.speed = speed;
        context.agent.destination = blackboard.moveToPosition;
        context.agent.updateRotation = updateRotation;
        context.agent.acceleration = acceleration;
        target = GameObject.FindGameObjectWithTag("MainCamera").transform;
        context.agent.SetDestination(target.position);
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        context.agent.SetDestination(target.position);
        if (context.agent.pathPending)
        {
            return State.Running;
        }

        if (context.agent.remainingDistance < tolerance)
        {
            return State.Success;
        }

        if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            return State.Failure;
        }

        return State.Running;
    }
}

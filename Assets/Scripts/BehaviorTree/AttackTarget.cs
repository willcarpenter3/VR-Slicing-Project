using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class AttackTarget : ActionNode
{
    public Transform target;
    private float heuristic;
    protected override void OnStart() {
        heuristic = context.gameObject.GetComponent<EnemyBehavior>().getHeuristic();
        target = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (heuristic < 0.66)
        {
            return State.Success;
        }

        return State.Running;
    }
}

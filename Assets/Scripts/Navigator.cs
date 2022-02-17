using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigator : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform goal;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = goal.position;
        Debug.Log("Follow Player: " + agent.velocity.magnitude);
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    public Vector3 getVelocity() { return agent.velocity; }
}

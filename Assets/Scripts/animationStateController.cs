using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    Navigator navigator;
    Vector3 velocity;
    int isWalkingHash;
    int isRunningHash;
    int isDeadHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navigator = GetComponent<Navigator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isDeadHash = Animator.StringToHash("isDead");
    }

    // Update is called once per frame
    void Update()
    {
        velocity = navigator.getVelocity();
        //animator.SetFloat( ,velocity.x)
        //animator.SetFloat( , velocity.z)
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool isDead = animator.GetBool(isDeadHash);
    }
}

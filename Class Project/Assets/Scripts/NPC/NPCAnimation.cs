using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAnimation : MonoBehaviour
{
    public Animator animator;
    private string currentState;

    public bool isWalking;
    public bool isRuning;
    public bool isFireing;
    public bool isSaluting;

    //Animation bool States
    const string FIREING = "isFireing"; 
    const string WALKING = "isWalking"; 
    const string RUNING = "isRunning"; 
    const string SALUTING = "isSaluting"; 
    const string STAIRS = "isStairs";
    //Animation name States 
    const string RIFLE_WALK = "Rifle Walk"; 
    const string RIFLE_RUN = "Rifle Run"; 
    const string RUNING_UP_STAIRS = "Running Up Stairs"; 
    const string DESCENDING_STAIRS = "Descending Stairs"; 

    const string HIT_REACTION_IDLE = "Hit Reaction Idle";
    const string HIT_REACTION_WALK = "Hit Reaction walk";
    const string HIT_REACTION_RUN = "Hit Reaction Run";

    const string SALUTE = "Put Back Rifle";

    const string DEAD = "Shoulder Hit And Fall";
    


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isWalking && isFireing)
            animator.SetBool(FIREING , true);
        else
             animator.SetBool(FIREING , false);


        if(isWalking)
        {
            animator.SetBool(WALKING , true);

            if(isFireing)
                animator.SetBool(FIREING, true);
            else
                animator.SetBool(FIREING , false);


            if(isRuning)
            {
                animator.SetBool(RUNING, true);
                 if(isFireing)
                animator.SetBool(FIREING , true);
            else
                animator.SetBool(FIREING , false);
            }
            else 
                animator.SetBool(RUNING , false);
        } 
        else 
        {
            animator.SetBool(RUNING , false);
            animator.SetBool(WALKING , false);
        }

        if(isSaluting)
        {
            animator.SetBool(SALUTING , true);
            isSaluting = false;
        }
    }

    public void npcIsHit()
    {
        if(!isWalking)
        {
            ReplayAnimation(HIT_REACTION_IDLE);
        }
        if(isWalking)
        {
            ReplayAnimation(HIT_REACTION_WALK);
        }
        if(isRuning)
        {
            ReplayAnimation(HIT_REACTION_RUN);
        }
    }

    public void IsStairs(bool isUpTheStairs)
    {
        if(isUpTheStairs)
            ChangeAnimationState(RUNING_UP_STAIRS);
        else
            ChangeAnimationState(DESCENDING_STAIRS);
    }

    public void PlayerMoving(bool isWalking)
    {
        if(isWalking)
            ChangeAnimationState(RIFLE_WALK);
        else
            ChangeAnimationState(RIFLE_RUN);
    }

    public void salute()
    {
        ChangeAnimationState(SALUTE);
    }

    public void PlayerDead()
    {
        ChangeAnimationState(DEAD);
    }

    public void ChangeAnimationState(string newState)
    {
        if(currentState == newState) return;

        animator.Play(newState);
        
        currentState = newState;
    }

    public void ReplayAnimation(string animation)
    {
        animator.Play(animation);
    }
}
 
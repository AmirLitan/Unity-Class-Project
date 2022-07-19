using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAnimation : MonoBehaviour
{
    public Animator animator;

    public bool isWalking;
    public bool isRuning;
    public bool isFireing;
    public bool isSaluting;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!isWalking && isFireing)
            animator.SetBool("isFireing", true);
        else
             animator.SetBool("isFireing", false);


        if(isWalking)
        {
            animator.SetBool("isWalking", true);

            if(isFireing)
                animator.SetBool("isFireing", true);
            else
                animator.SetBool("isFireing", false);


            if(isRuning)
            {
                animator.SetBool("isRunning", true);

                 if(isFireing)
                animator.SetBool("isFireing", true);
            else
                animator.SetBool("isFireing", false);
            }
            else 
                animator.SetBool("isRunning", false);
        } 
        else 
        {
            animator.SetBool("isWalking", false);
        }

        if(isSaluting)
        {
            animator.SetBool("isSaluting", true);
            isSaluting = false;
        }
    }

    public void npcIsHit()
    {
        if(!isWalking)
        {
            //hit idle animation
        }
        if(isWalking)
        {
            //hit walk animation
        }
        if(isRuning)
        {
            //hit run animation
        }
    }
}
 
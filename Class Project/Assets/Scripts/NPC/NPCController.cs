using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : Interactable
{
    public GameObject player;
    public LayerMask whatIsPlayer;
    public NPCAnimation npcAnimation;
    private NavMeshAgent nav;

    //States
    public float sightRange;
    public float attackRange;
    private bool playerInSightRange;
    private bool playerInAttackRange;

    [SerializeField] private bool isPlayerInteract;
    [SerializeField] private bool isUse;
    [SerializeField] private bool isToPlayer;
    [SerializeField] private bool isAttack;
    private float distanceFromPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        npcAnimation = GetComponent<NPCAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position,sightRange,whatIsPlayer);
        distanceFromPlayer = Vector3.Distance(player.transform.position,transform.position);

        if(playerInSightRange){
            Debug.Log(distanceFromPlayer);
            if(isToPlayer)
            {
                if(distanceFromPlayer > 10)
                {
                    nav.destination = player.transform.position;
                    if(nav.velocity.magnitude > 3)
                    {
                        npcAnimation.isWalking = true;
                        if(nav.velocity.magnitude > 6)
                            npcAnimation.isRuning = true;
                        else
                            npcAnimation.isRuning = false;
                    }
                    else 
                        npcAnimation.isWalking = false;
                }
                else
                {
                    nav.destination = transform.position;
                    npcAnimation.isWalking = false;
                    npcAnimation.isRuning = false;
                }
            }
        }
    }

    public void ToPlayer()
    {
        isToPlayer = true;
    }

    public void Attack()
    {
        //npc will attack enmiy in player deraction
    }

    public void Use()
    {
        if(isPlayerInteract)
        {
            //action to npc from player only if selected
            //npc get icon to know his was selected 
            //if player is idle animation will hapand 
            isPlayerInteract = false;
        }
    }

    protected override void Interact()
    {
        isPlayerInteract = true;
    }

}

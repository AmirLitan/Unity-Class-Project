using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LeaderNPCController : MonoBehaviour
{
    public bool isLeader;
    
    public LayerMask whatIsEnemy;
    public LayerMask whatIsPlayer;
    public NPCAnimation npcAnimation;
    private NavMeshAgent nav;

    //Goal
    public GameObject goalObject;

    //Interactables
    public LayerMask whatIsInteractables;
    public GameObject look;
    public float distance = 10;
    private RaycastHit hitInfo;
    private bool waitAction;

    //States
    public float sightRange;
    public float attackRange;
    [SerializeField] private bool enemyInSightRange;
    [SerializeField] private bool enemyInAttackRange; 
    [SerializeField] private bool playerInSightRange;
    [SerializeField] private bool playerInAttackRange;

    [SerializeField] private bool isInteract;
    [SerializeField] private bool isUse;
    [SerializeField] private bool isPotroll;
    [SerializeField] private bool isAttack;
    [SerializeField] private bool enemyInSight;
    [SerializeField] private bool playerInSight;

    //Health
    [SerializeField] private float health = 200;
    [SerializeField] private bool dead;

    private bool salute;

    private float distanceFromLeader;
    private float distanceFromEnemy;

    [SerializeField] float timeBetweenAttack = 2f;

    private NPCController enemy;
    private PlayerUI player;
    private bool alreadyAttacked;

    [SerializeField] private Transform bulletProjectile;
    [SerializeField] private Transform spawnBulletPosistion; 
    [SerializeField] private Transform vfxFire;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        npcAnimation = GetComponent<NPCAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
         if(!dead)
         {

            interactable();

            if(!isInteract)
            {
                enemyInSightRange = Physics.CheckSphere(transform.position,sightRange,whatIsEnemy);
                playerInSightRange = Physics.CheckSphere(transform.position,sightRange,whatIsPlayer);

                enemyInAttackRange = Physics.CheckSphere(transform.position,attackRange,whatIsEnemy);    
                playerInAttackRange = Physics.CheckSphere(transform.position,attackRange,whatIsPlayer);

                if(enemyInSightRange || playerInSightRange) 
                {
                    NpcAttack(); 
                }
                else 
                {
                    NcpToObject(goalObject);
                }
            }
            
         }
        // {
        //     leaderInSightRange = Physics.CheckSphere(transform.position,sightRange,whatIsLeader);
            

        //     distanceFromLeader = Vector3.Distance(leader.transform.position,transform.position);
        //     distanceFromEnemy = Vector3.Distance(leader.transform.position,transform.position);

        //     if(!salute && distanceFromLeader < 30)
        //     {
        //         salute = false;
        //         nav.destination = transform.position;
        //         npcAnimation.salute();
        //     }
        //     if(leaderInSightRange && !enemyInSightRange)
        //     {
        //         NpcToLeader(leader); 
        //     }
        //     else 
        //         StopWalking();
                
        // }
   
    }

    private void interactable()
    {
        Ray ray = new Ray(look.transform.position, look.transform.forward);
        Debug.DrawRay(ray.origin,ray.direction * distance);
        if(Physics.Raycast(ray, out hitInfo, distance,whatIsInteractables))
        {
            Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
            if(hitInfo.collider.GetComponent<Interactable>() != null)
            {
                StopWalking();
                isInteract = true;
                if(!waitAction)
                {
                interactable.BaseInteract();
                waitAction = true;
                Invoke(nameof(ResumeAction),5);
                Invoke(nameof(ResumeGoal),2);
                }
            }
        }
    }
    
    private void ResumeAction()
    {
        waitAction = false;
    }
    private void ResumeGoal()
    {
        isInteract = false;
    }

    private void NpcAttack()
    {
        IfPlayerInSight();
        IfEnemyInSight();
    }

    private void IfPlayerInSight()
    {
         if(!playerInSight)
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position,sightRange, whatIsPlayer);
                    foreach (Collider collider in colliderArray )
                    {
                        if(collider.TryGetComponent<PlayerUI>(out PlayerUI player))
                        {   
                            if(!player.isDead())
                            {
                                this.player = player;
                                playerInSight = true;
                            }
                        }
                    }
        }
        else if(!player.isDead())
        {
            AttackEnemy(player.transform);
        } 
        else
        {
            playerInSight = false;
            npcAnimation.isFireing = false;
        }
    }

    private void IfEnemyInSight()
    {
        if(!enemyInSight)
        {
            npcAnimation.isFireing = false;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position,sightRange, whatIsEnemy);
                    foreach (Collider collider in colliderArray )
                    {
                        if(collider.TryGetComponent<NPCController>(out NPCController npc))
                        {   
                            if(!npc.isDead())
                            {
                                enemy = npc;
                                enemyInSight = true;
                            }
                        }
                    }
        }
        else if(!enemy.isDead())
        {
            AttackEnemy(enemy.transform);
        } 
        else
        {
            enemyInSight = false;
            npcAnimation.isFireing = false;
        }
    }

    private void AttackEnemy(Transform t)
    {
        if(enemyInAttackRange)
        {
            StopWalking();
            transform.LookAt(t);
            if(!alreadyAttacked)
            {
                Instantiate(vfxFire, spawnBulletPosistion.position , Quaternion.LookRotation(spawnBulletPosistion.position, Vector3.up));
                Instantiate(bulletProjectile, spawnBulletPosistion.position , Quaternion.LookRotation(spawnBulletPosistion.forward , Vector3.up));

                npcAnimation.isFireing = true;
                
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttack);
            }
        }
        else
        {
            Debug.Log("enemyInAttackRange = false");
            npcAnimation.isFireing = false;
            nav.destination = t.position;
            if(nav.velocity.magnitude > 1)
            {
                npcAnimation.isWalking = true;
                if(nav.velocity.magnitude > 12)
                {

                    npcAnimation.isRuning = true;
                }
                else
                {
                    npcAnimation.isRuning = false;
                }
            }
            else
            {
                npcAnimation.isWalking = false;
            }
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void NcpToObject(GameObject goal)
    {
        nav.destination = goal.transform.position;
        float distanceFromGoal = Vector3.Distance(goal.transform.position,transform.position);
            if(distanceFromGoal > 15)
            {
                    if(nav.velocity.magnitude > 1)
                    {
                        if(nav.velocity.y <  -10)
                        {
                            npcAnimation.IsStairs(false);
                        } 
                        else if (nav.velocity.y > 10) 
                        {
                            npcAnimation.IsStairs(true);
                        }
                        else
                        {
                        npcAnimation.isWalking = true;
                        if(nav.velocity.magnitude > 12)
                        {
                            npcAnimation.isRuning = true;
                        }
                        else
                        {
                            npcAnimation.isRuning = false;
                        }
                        }
                    }
                    else 
                    {
                        npcAnimation.isWalking = false;  
                    }
        }
        else
        {
            StopWalking();
        }

    }

    // public void NpcToLeader(GameObject gameObject)
    // {
    //     if(isToLeader)
    //     {
    //         if(distanceFromLeader > 15)
    //         {
    //             nav.destination = gameObject.transform.position;
    //             if(nav.velocity.magnitude > 1)
    //             {
    //                 if(nav.velocity.y <  -10)
    //                 {
    //                     npcAnimation.IsStairs(false);
    //                 } 
    //                 else if (nav.velocity.y > 10) 
    //                 {
    //                     npcAnimation.IsStairs(true);
    //                 }
    //                 else
    //                 {
    //                 npcAnimation.isWalking = true;
    //                 if(nav.velocity.magnitude > 12)
    //                 {
    //                     npcAnimation.isRuning = true;
    //                 }
    //                 else
    //                 {
    //                     npcAnimation.isRuning = false;
    //                 }
    //                 }
    //             }
    //             else 
    //             {
    //                 npcAnimation.isWalking = false;  
    //             }
    //         }
    //         else
    //         {
    //            StopWalking();
    //         }
    //     }
    //     else if(isAttack)
    //     {
           
    //     }
    //     else
    //     {
    //        StopWalking();
    //     }
    // }

    public void StopWalking()
    {
        nav.destination = transform.position;
        npcAnimation.isRuning = false;
        npcAnimation.isWalking = false;
    }

    // public void ToPlayer()
    // {
    //     isToLeader = !isToLeader;
    //     isAttack = false;
    //     leader.GetComponent<PlayerUI>().UiAttack(isAttack);
    //     leader.GetComponent<PlayerUI>().UiToPlayer(isToLeader);
    // }

    // public void Attack()
    // {
    //     //npc will attack enmiy in player deraction
    //     isAttack = !isAttack;
    //     isToLeader = false;
    //     leader.GetComponent<PlayerUI>().UiToPlayer(isToLeader);
    //     leader.GetComponent<PlayerUI>().UiAttack(isAttack);
    // }

    // public void Use()
    // {
    //     if(isPlayerInteract)
    //     {
    //         //action to npc from player only if selected
    //         //npc get icon to know his was selected 
    //         //if player is idle animation will hapand 
    //         isPlayerInteract = false;
    //     }
    // }

    public bool isDead()
    {
        return dead;
    }

    public void getHit(int damage)
    {
        health = health - damage;
        if(health <= 0)
        {
            dead = true;
            this.gameObject.layer = LayerMask.NameToLayer("Default");
            this.gameObject.GetComponent<CharacterController>().enabled = false;
            npcAnimation.PlayerDead();
        }
        else
        {
            npcAnimation.npcIsHit();
        }
    }
}

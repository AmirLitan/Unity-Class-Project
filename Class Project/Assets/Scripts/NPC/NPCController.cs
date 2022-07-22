using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public GameObject player;
    public LayerMask whatIsPlayer;
    public LayerMask whatIsEnemy;
    public NPCAnimation npcAnimation;
    private NavMeshAgent nav;


    //States
    public float sightRange;
    public float attackRange;
    [SerializeField] private bool playerInSightRange;
    [SerializeField] private bool enemyInSightRange;
    [SerializeField] private bool enemyInAttackRange;

    [SerializeField] private bool isPlayerInteract;
    [SerializeField] private bool isUse;
    [SerializeField] private bool isToPlayer;
    [SerializeField] private bool isPotroll;
    [SerializeField] private bool isAttack;
    [SerializeField] private bool enemyInSight;

    //Health
    [SerializeField] private float health = 100;
    [SerializeField] private bool dead;

    private bool salute;

    private float distanceFromPlayer;
    private float distanceFromEnemy;

    [SerializeField] float timeBetweenAttack = 2f;

    private NPCController enemy;
    private bool alreadyAttacked;

    [SerializeField] private Transform bulletProjectile;
    [SerializeField] private Transform spawnBulletPosistion; 
    [SerializeField] private Transform vfxFire;

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
        if(!dead)
        {

            playerInSightRange = Physics.CheckSphere(transform.position,sightRange,whatIsPlayer);
            
            enemyInSightRange = Physics.CheckSphere(transform.position,sightRange,whatIsEnemy);
            enemyInAttackRange = Physics.CheckSphere(transform.position,sightRange,whatIsEnemy);
            distanceFromPlayer = Vector3.Distance(player.transform.position,transform.position);
            distanceFromEnemy = Vector3.Distance(player.transform.position,transform.position);
            if(!salute && distanceFromPlayer < 30)
            {
                salute = false;
                nav.destination = transform.position;
                npcAnimation.salute();
            }
            if(playerInSightRange && !enemyInSightRange)
            {
                NpcToPlayer(player); 
            }
            else 
                StopWalking();
                
            if(enemyInSightRange) 
                NpcAttack(); 
            else if(!isToPlayer)
            {
                StopWalking();
                npcAnimation.isFireing = false;
            } 
        }
   
    }

    private void NpcAttack()
    {
        if(!enemyInSight)
        {
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
            AttackEnemy(enemy);
        } 
        else
        {
            enemyInSight = false;
            npcAnimation.isFireing = false;
        }
    }

    private void AttackEnemy(NPCController npc)
    {
        if(enemyInAttackRange)
        {
            StopWalking();
            transform.LookAt(npc.transform);
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
            npcAnimation.isFireing = false;
            nav.destination = npc.transform.position;
            if(nav.velocity.magnitude > 1)
            {
                npcAnimation.isWalking = true;
                if(nav.velocity.magnitude > 12)
                        npcAnimation.isRuning = true;
                    else
                    {
                        npcAnimation.isRuning = false;
                    }
            }
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void NcpToObject(GameObject gameObject)
    {
        nav.destination = gameObject.transform.position;
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

    public void NpcToPlayer(GameObject gameObject)
    {
        if(isToPlayer)
        {
            if(distanceFromPlayer > 15)
            {
                nav.destination = gameObject.transform.position;
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
        else if(isAttack)
        {
           
        }
        else
        {
           StopWalking();
        }
    }

    public void StopWalking()
    {
        nav.destination = transform.position;
        npcAnimation.isRuning = false;
        npcAnimation.isWalking = false;
    }

    public void ToPlayer()
    {
        isToPlayer = !isToPlayer;
        isAttack = false;
        player.GetComponent<PlayerUI>().UiAttack(isAttack);
        player.GetComponent<PlayerUI>().UiToPlayer(isToPlayer);
    }

    public void Attack()
    {
        //npc will attack enmiy in player deraction
        isAttack = !isAttack;
        isToPlayer = false;
        player.GetComponent<PlayerUI>().UiToPlayer(isToPlayer);
        player.GetComponent<PlayerUI>().UiAttack(isAttack);
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
            npcAnimation.PlayerDead();
        }
        else
        {
            npcAnimation.npcIsHit();
        }
    }
}

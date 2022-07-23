using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Transform vfxHitOpject;
    private Rigidbody bulletRigidbody;

    [SerializeField] int damage = 20;



    private void Awake() 
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start() 
    {
        float speed = 40f;
        bulletRigidbody.velocity = transform.forward * speed;    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerUI>(out PlayerUI player))
        {
            player.getHit(damage);
        }
        if(other.TryGetComponent<NPCController>(out NPCController frandly))
        {
            frandly.getHit(damage);
        }
        if(other.TryGetComponent<EnemyNPCController>(out EnemyNPCController enemy))
        {
            enemy.getHit(damage);
        }
        if(other.TryGetComponent<LeaderNPCController>(out LeaderNPCController leader))
        {
            leader.getHit(damage);
        }
        Instantiate(vfxHitOpject , transform.position , Quaternion.identity);
        Destroy(gameObject);
    }



}

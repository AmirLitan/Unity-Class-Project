using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Transform vfxHitOpject;
    private Rigidbody bulletRigidbody;



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
        Instantiate(vfxHitOpject , transform.position , Quaternion.identity);
        Destroy(gameObject);
    }



}

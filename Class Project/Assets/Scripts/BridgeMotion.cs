using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeMotion : MonoBehaviour
{
    private Animator BridgeAnimator;
    private AudioSource sound;
    private bool isTrigger = false;
    
    // Start is called before the first frame update
    void Start()
    {
        BridgeAnimator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!isTrigger){
        BridgeAnimator.SetBool("BridgeDown",true);
        sound.PlayDelayed(0.2f);
        isTrigger = true;
        }
    }
}

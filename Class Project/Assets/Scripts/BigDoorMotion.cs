using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigDoorMotion : MonoBehaviour  
{
    
    private Animator doorsAnimator;
    private AudioSource sound;
    private bool doorOpen;

    public GameObject player;
    public GameObject keyHole;
    public string messageOnBox;
    private bool messageActive;
    // Start is called before the first frame update
    void Start()
    {
        messageActive = false;
        doorsAnimator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        doorOpen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(keyHole.activeSelf && !doorOpen){
        doorsAnimator.SetBool("DoorOpen", true);
        sound.PlayDelayed(0.6f);
        doorOpen = true;
        }
        else
        {
            if(messageActive)
            {
                messageActive = true;
                player.GetComponent<PlayerInteract>().SendMessage("Need key to open");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(messageActive){
            messageActive = false;
            player.GetComponent<PlayerInteract>().SendMessage("");
        }
        if(!doorOpen && keyHole.activeSelf)
        {
            doorsAnimator.SetBool("DoorOpen", false);
            sound.PlayDelayed(0.6f);
            doorOpen = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    [SerializeField] private bool isKeyInBox;
    [SerializeField] private GameObject player;
    [SerializeField] public GameObject key;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
         if(isKeyInBox)
        setMessage("Press F to pick key");
        else 
        setMessage("Press F to put key");
        
    }

     protected override void Interact()
    {
        if(isKeyInBox)
        {
            player.GetComponent<PlayerInteract>().setKeyInHand();
            isKeyInBox = false;
            key.SetActive(false);
        } 
        else if(player.GetComponent<PlayerInteract>().isKeyInHand)
        {
            player.GetComponent<PlayerInteract>().setKeyInHand();
            isKeyInBox = true;
            key.SetActive(true);
        }
    }

    public bool getIskeyIn()
    {
        return isKeyInBox;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDoors : Interactable
{

    public GameObject keyHole;
    public GameObject bigDoors;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        bigDoors.GetComponent<Animator>().SetBool("DoorOpen", keyHole.GetComponent<Key>().getIskeyIn());
    }

      protected override void Interact()
    {
        
    }
}

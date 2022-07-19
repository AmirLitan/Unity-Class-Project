using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestWithKey : Interactable
{
    [SerializeField] private GameObject chest;
    private bool ChestOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if(!ChestOpen)
        setMessage("Press F to open");
        else 
        setMessage("Press F to close");
    }

    protected override void Interact()
    {
        ChestOpen = !ChestOpen;
        chest.GetComponent<Animator>().SetBool("BoxOpen", ChestOpen);
    }
}

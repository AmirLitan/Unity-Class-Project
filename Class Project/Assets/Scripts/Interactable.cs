using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private string message;
    
    //this function will ve called from our player.
    public void BaseInteract()
    {
        Interact();
    }
    protected virtual void Interact()
    {
        //this is a tamplate function
    }

    public void setMessage(string message)
    {
        this.message = message;
    }

    public string getMessage()
    {
        return message;
    }
}

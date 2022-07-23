using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTheGoldCup : Interactable
{

     void Update()
    {
        setMessage("Press F to WIN!");
    }
     protected override void Interact()
    {
        SceneManager.LoadScene(3);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickBoxKey : MonoBehaviour
{
    public GameObject KeyInBox;
    public GameObject keyInDoor;
    public GameObject keyInHand;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnMouseDown() 
    {
        if(!keyInDoor.activeSelf)
        {
            if(KeyInBox.activeSelf){
            KeyInBox.SetActive(false);
            keyInHand.SetActive(true);
            }else
            {
            KeyInBox.SetActive(true);
            keyInHand.SetActive(false);  
            }
        }
    }
}

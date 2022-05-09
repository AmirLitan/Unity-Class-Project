using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickDoorKey : MonoBehaviour
{
    public GameObject keyInDoor;
    public GameObject keyInBox;
    public GameObject keyInHand;
    [SerializeField] private bool boolkeyInBox;
    void Start()
    {
        
    }

    void Update()
    {
        boolkeyInBox = keyInBox.activeSelf;
    }
    private void OnMouseDown() 
    {
        if(!keyInBox.activeSelf){
            if(keyInDoor.activeSelf){
            keyInDoor.SetActive(false);
            keyInHand.SetActive(true);
            }
            else
            {
            keyInDoor.SetActive(true);
            keyInHand.SetActive(false);  
            }
        }
    }
}

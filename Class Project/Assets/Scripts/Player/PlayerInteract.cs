using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private float distance = 15f;
    [SerializeField] public GameObject key; 

    public bool isKeyInHand;
    private Camera cam;
    private PlayerUI playerUI;
    private InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
        isKeyInHand = false;
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin,ray.direction * distance);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, distance,mask))
        {
            Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
            if(hitInfo.collider.GetComponent<Interactable>() != null)
            {
                playerUI.UpdateText(interactable.getMessage());
                if(inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }

            }
        }    
    }

    public void setKeyInHand()
    {
        isKeyInHand = !isKeyInHand;
        if(isKeyInHand)
        {
            key.SetActive(true);
        } 
        else
        {
            key.SetActive(false);
        }
    }


}

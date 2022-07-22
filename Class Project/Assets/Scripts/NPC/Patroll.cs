using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Patroll : MonoBehaviour
{

    public float distance = 20;
    public GameObject look;
    public GameObject potrollStartPosition;
    public GameObject potrollEndPosition;
    [SerializeField] private LayerMask mask;
    private RaycastHit hitInfo;
    private bool action;
    private bool potrollReset;
    public float timeBetweenAction = 5;
    public float timeReset = 3;
    public NPCController npcController;
    private NavMeshAgent nav;
    // Start is called before the first frame update
    void Start()
    {
        potrollReset = true;
        npcController = GetComponent<NPCController>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Invoke(nameof(startPotroll), 1);
        if(!potrollReset)
        {
            Debug.Log("to opject");
            npcController.NcpToObject(potrollEndPosition);
            nav.destination = potrollEndPosition.transform.position;
        }
        Ray ray = new Ray(look.transform.position, look.transform.forward);
        Debug.DrawRay(ray.origin,ray.direction * distance);
        if(Physics.Raycast(ray, out hitInfo, distance,mask))
        {
            Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
            if(hitInfo.collider.GetComponent<Interactable>() != null && !action)
            {
                npcController.StopWalking();
                interactable.BaseInteract();
                action = true;
                potrollReset = true;
                Invoke(nameof(ResetAction), timeBetweenAction);
                Invoke(nameof(Reset), timeReset);
            }
        }
    }

    private void ResetAction()
    {
        action = false;
    }
    private void Reset() 
    {
        potrollReset = false;
    }
    private void startPotroll()
    {
        potrollReset = false;
    }
}

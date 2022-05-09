using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBox : MonoBehaviour
{
    public Animator animator;
    private AudioSource sound;
    private bool BoxIsClosed;
    private bool messageActive;

    public Text messageOnBox;
    public GameObject aCamera;
    public GameObject upperCollider;
    // Start is called before the first frame update
    void Start()
    {
        messageActive = false;
        BoxIsClosed = true;
       // animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(aCamera.transform.position, aCamera.transform.forward, out hit);
        if (hit.transform.gameObject == upperCollider.gameObject && hit.distance < 15)
        {
            if (!messageActive)
            {
                setMessage();
                messageActive = true;
            }
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(OpenCloseChest());
            }
        }
        else
        {
            if (messageActive)
            {
                messageOnBox.text = "";
                messageActive = false;
            }
        }
    }

    IEnumerator OpenCloseChest()
    {
        animator.SetBool("BoxOpen", BoxIsClosed);
        sound.PlayDelayed(0.8f);
        BoxIsClosed = !BoxIsClosed;
        
        yield return new WaitForSeconds(3);
        setMessage();
    }
    private void setMessage()
    {
        if (!animator.GetBool("BoxOpen"))
        {
            messageOnBox.text = "Press [F] to open chest";
        }
        else
        {
            messageOnBox.text = "Press [F] to close chest";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    private CharacterController cController;
    private float speed = 15;
    private float angularSpeed = 100;
    private float rotationAboutY = 0;
    private float rotationAboutX = 0;
    private AudioSource stepsSound;
    public GameObject aCamera;

    // Start is called before the first frame update
    void Start()
    {
        cController = GetComponent<CharacterController>();  
        stepsSound = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        float dx,dz;

        rotationAboutX -= Input.GetAxis("Mouse Y")*angularSpeed*Time.deltaTime;
        aCamera.transform.localEulerAngles = new Vector3(rotationAboutX,0,0);

        rotationAboutY += Input.GetAxis("Mouse X")*angularSpeed*Time.deltaTime;
        transform.localEulerAngles = new Vector3(0,rotationAboutY,0);

        dx = Input.GetAxis("Horizontal")*speed*Time.deltaTime;
        dz = Input.GetAxis("Vertical")*speed*Time.deltaTime;

        Vector3 motion = new Vector3(dx,-1,dz);
        motion = transform.TransformDirection(motion);
        cController.Move(motion);
        if(Mathf.Abs(motion.z)>0.01f || Mathf.Abs(motion.x)>0.01f)
        {
            if(!stepsSound.isPlaying)
            {
                stepsSound.Play();
            }
        }
    }
}

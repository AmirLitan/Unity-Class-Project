using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot ;

    private PlayerMotor motor;
    private PlayerLook look;
    private PlayerWeapons weapon;
    public GameObject npc1;
    public GameObject npc2;
    public GameObject npc3;
    public GameObject npc4;
    private NPCController npcController1;
    private NPCController npcController2;
    private NPCController npcController3;
    private NPCController npcController4;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;  

        motor = GetComponent<PlayerMotor>();  
        look = GetComponent<PlayerLook>();
        weapon = GetComponent<PlayerWeapons>();
        npcController1 = npc1.GetComponent<NPCController>();
        npcController2 = npc2.GetComponent<NPCController>();
        npcController3 = npc3.GetComponent<NPCController>();
        npcController4 = npc4.GetComponent<NPCController>();

        onFoot.Jump.performed += ctx => motor.Jump(); 
        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Sprint.performed += ctx => motor.Sprint();
        onFoot.FireWeapon.performed += ctx => weapon.Fire();

        onFoot.NpcToPlayer.performed += ctx => npcController1.ToPlayer();
        onFoot.NpcAttack.performed += ctx => npcController1.Attack();

        onFoot.NpcToPlayer.performed += ctx => npcController2.ToPlayer();
        onFoot.NpcAttack.performed += ctx => npcController2.Attack();


        onFoot.NpcToPlayer.performed += ctx => npcController3.ToPlayer();
        onFoot.NpcAttack.performed += ctx => npcController3.Attack();


        onFoot.NpcToPlayer.performed += ctx => npcController4.ToPlayer();
        onFoot.NpcAttack.performed += ctx => npcController4.Attack();
        
    }

    // Update is called once per frame
   private void FixedUpdate() 
   {
       //tell the playermotor to move using the value from our movement action
       motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
   }

   private void LateUpdate() 
   {
         look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
   }

    private void OnEnable()
    {
        onFoot.Enable();    
    }
 
    private void OnDisable() 
    {
        onFoot.Disable();    
    }
}

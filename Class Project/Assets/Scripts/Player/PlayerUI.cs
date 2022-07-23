using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private RawImage follow;
    [SerializeField] private RawImage attack;

    //Health
    [SerializeField] private float health = 200;
    [SerializeField] private bool dead;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   public void UpdateText(string prompMessage)
   {
       promptText.text = prompMessage;
   }

   public void UiToPlayer(bool isToPlayer)
   {
        if(isToPlayer)
            follow.color = Color.green;
        else
            follow.color = Color.red;  
   }

   public void UiAttack(bool isAttack)
   {
        if(isAttack)
            attack.color = Color.green;
        else
            attack.color = Color.red;  
   }

      public bool isDead()
    {
        return dead;
    }

    public void getHit(int damage)
    {
        health = health - damage;
        if(health <= 0)
        {
            dead = true;
        }
        else
        {
            //ui sow how mach health player have
        }
    }
}

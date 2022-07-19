using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] private LayerMask aimColliderMask = new LayerMask();  
    //[SerializeField] private Transform debugTrandform;
    [SerializeField] private Transform bulletProjectile;
    [SerializeField] private Transform spawnBulletPosistion; 
    [SerializeField] private Transform vfxFire;

    public Animator fireAnimation;

     private bool isFiring;
    // Start is called before the first frame update
    void Start()
    {
        isFiring = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(fireAnimation.GetBool("isFire"))
        {
            fireAnimation.SetBool("isFire", false);
        }

        Vector3 mousWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f , Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderMask))
        {
            //debugTrandform.position = raycastHit.point;
            mousWorldPosition = raycastHit.point;
        }

        if(isFiring)
        {
            Vector3 aimDir = (mousWorldPosition - spawnBulletPosistion.position).normalized;
            Instantiate(vfxFire, spawnBulletPosistion.position , Quaternion.LookRotation(aimDir, Vector3.up));
            Instantiate(bulletProjectile, spawnBulletPosistion.position , Quaternion.LookRotation(aimDir, Vector3.up));
           // fireAnimation.SetBool("isFire", false);
            fireAnimation.SetBool("isFire", true);
            isFiring = false;
        }

    }

    public void Fire()
    {
        isFiring = true;
    }
}

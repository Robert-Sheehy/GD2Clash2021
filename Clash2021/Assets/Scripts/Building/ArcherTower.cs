using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : Building
{
    bool isAttacking = false;

    public GameObject Arrow;


    // Start is called before the first frame update
    new
    
    void Start()
    {
        base.Start();
        Melee_distance = 3.5f;
        is_offensive_building = true;
        DPS = 100;
        max_distance_ranged = 20f;
        attack_time_interval = 2.5f;
    }
   
    // Update is called once per frame
    new
    
    void Update()
    {
        base.Update();

        
        if (current_state == Unit_States.Attacking && current_target &&  within_range(current_target)){
            isAttacking = true;
            
            GameObject new_Arrow = Instantiate(Arrow,transform.position + new Vector3(0,1.5f), Quaternion.identity);
            projectile_script NewArrowScript = new_Arrow.GetComponent<projectile_script>();

            NewArrowScript.setupProjectile(transform.position,current_target.transform.position,2,15,theManager,DPS);
        }

        if (current_state == Unit_States.Idle){
            isAttacking = false;
        }

    }

    internal override void attack(int dmg)
    {
        base.attack(dmg);

        isAttacking = true;
    }

}

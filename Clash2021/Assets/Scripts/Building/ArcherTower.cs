using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : Building
{
    bool isAttacking = false;


    // Start is called before the first frame update
    new
    
    void Start()
    {
    MHP = 250;
    DPS = 110;
    base.Start();
    min_distance_ranged = 0;
    max_distance_ranged = 30;
    attack_time_interval = 2;
    
    }
   
    // Update is called once per frame
    new
    
    void Update()
    {
        if (isAttacking){

        }
        
        if (current_state != Building_States.Attacking)
        {
            if (isAttacking)
            {
                isAttacking = false;
            }
        }

        base.Update();
    }

    internal override void attack(int dmg)
    {
        base.attack(dmg);

        isAttacking = true;
    }

}

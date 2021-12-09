using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript :Building, IHealth
{
    public GameObject CannonBall;

    ParticleSystem muzzle_flash;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        Melee_distance = 3.5f;
        is_offensive_building = true;
        DPS = 100;
        max_distance_ranged = 20f;
        attack_time_interval = 2.5f;
        muzzle_flash = GetComponent<ParticleSystem>();
        muzzle_flash.Play();
       
    }
    new 
    // Update is called once per frame
    void Update()
    {

        base.Update();
        if (current_state == Unit_States.Attacking && current_target && attack_timer <= 0f && within_range(current_target))
        {


            GameObject new_CannonBall = Instantiate(CannonBall,
                  transform.position + new Vector3(0,1.5f), Quaternion.identity);
            projectile_script NewCannonballScript = new_CannonBall.GetComponent<projectile_script>();

            NewCannonballScript.setupProjectile(transform.position,current_target.transform.position,0,10,theManager,DPS);

}

        if (Input.GetKeyDown(KeyCode.F))
            attack(5);
    }


    internal override void attack(int dmg)
    {

       // current_target.takeDamage(dmg);
        muzzle_flash.Play();

    }



}
